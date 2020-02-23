import { publish, SET_APP_STATUS } from "app/_sys/pubsub";
import { AppStatus } from "app/types";

interface IResponse<T> {
    ok: boolean,
    status: number,
    data?: T
}

interface IConnectionResponse extends ISchema { 
    name: string,
    schemas: {
        names: Array<string>,
        selected: string
    },
}

interface ISchemaResponse extends ISchema { 
    name: string
}

export interface ISchema { 
    routines: Array<IRoutineInfo>,
    scripts: Array<IScriptInfo>,
    tables: Array<string>,
    views: Array<string>
}

export interface IConnectionInfo {
    name: string, 
    version: string,
    host: string, 
    port: number, 
    database: string,
    user: string 
}

export interface IRoutineInfo {
    id: string,
    language: string,
    name: string,
    type: string
}

export interface IScriptInfo {
    id: string,
    title: string,
    comment: string,
    timestamp: string
}

interface IScript extends IScriptInfo {
    schema: string,
    content: string,
    viewState: string
}

interface IInitialResponse { 
    connections: Array<IConnectionInfo>
}

const _createResponse:<T> (response: Response, data?: T) => IResponse<T> = (response, data) => Object({ok: response.ok, status: response.status, data: data});

const _fetchAndPublishStatus:<T> (url: string) => Promise<IResponse<T>> = async url => {
    publish(SET_APP_STATUS, AppStatus.BUSY);
    try {
        const response = await fetch(url);
        if (!response.ok) {
            publish(SET_APP_STATUS, AppStatus.ERROR, response.status);
            return _createResponse(response);
        }
        return _createResponse(response, await response.json());
    } catch (error) {
        publish(SET_APP_STATUS, AppStatus.ERROR, error.message);
        throw error;
    }
}

const _fetch:<T> (url: string) => Promise<IResponse<T>> = async url => {
    const response = await fetch(url);
    if (!response.ok) {
        return _createResponse(response);
    }
    return _createResponse(response, await response.json());
}

let _currentSchema;
const getCurrentSchema = () => _currentSchema;

export const fetchInitial: () => Promise<IResponse<IInitialResponse>> = async () => 
    _fetchAndPublishStatus<IInitialResponse>("api/initial");

export const fetchConnection: (name: string) => Promise<IResponse<IConnectionResponse>> = async name => {
    const result = _fetchAndPublishStatus<IConnectionResponse>(`api/connection/${name}`);
    _currentSchema = (await result).data.schemas.selected;
    return result;
}

export const fetchSchema: (schema: string) => Promise<IResponse<ISchemaResponse>> = async schema => {
    const result = _fetchAndPublishStatus<ISchemaResponse>(`api/schema/${schema}`);
    _currentSchema = (await result).data.name;
    return result;
}

export const createScript: () => Promise<IResponse<IScript>> = async () => _fetch(`api/create-script/${getCurrentSchema()}`);