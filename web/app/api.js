define(["require", "exports", "app/_sys/pubsub", "vs/editor/editor.main"], function (require, exports, pubsub_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.ScriptId = id => `${Keys.SCRIPTS}${id}`;
    exports.TableId = id => `${Keys.TABLES}${id}`;
    exports.ViewId = id => `${Keys.VIEWS}${id}`;
    exports.RoutineId = id => `${Keys.ROUTINES}${id}`;
    exports.classes = { active: "active", sticky: "sticky", docked: "docked" };
    var Position;
    (function (Position) {
        Position["LEFT"] = "left";
        Position["RIGHT"] = "right";
    })(Position = exports.Position || (exports.Position = {}));
    var Themes;
    (function (Themes) {
        Themes["DARK"] = "dark";
        Themes["LIGHT"] = "light";
    })(Themes = exports.Themes || (exports.Themes = {}));
    var AppStatus;
    (function (AppStatus) {
        AppStatus[AppStatus["READY"] = 0] = "READY";
        AppStatus[AppStatus["BUSY"] = 1] = "BUSY";
        AppStatus[AppStatus["ERROR"] = 2] = "ERROR";
        AppStatus[AppStatus["NO_CONNECTION"] = 3] = "NO_CONNECTION";
    })(AppStatus = exports.AppStatus || (exports.AppStatus = {}));
    var Keys;
    (function (Keys) {
        Keys["SCRIPTS"] = "scripts";
        Keys["TABLES"] = "tables";
        Keys["VIEWS"] = "views";
        Keys["ROUTINES"] = "routines";
        Keys["SEARCH"] = "search";
    })(Keys = exports.Keys || (exports.Keys = {}));
    var Languages;
    (function (Languages) {
        Languages["PGSQL"] = "pgsql";
    })(Languages = exports.Languages || (exports.Languages = {}));
    const _createResponse = (response, data) => Object({ ok: response.ok, status: response.status, data: data });
    const _fetchAndPublishStatus = async (url, init) => {
        pubsub_1.publish(pubsub_1.SET_APP_STATUS, AppStatus.BUSY);
        try {
            const response = await fetch(url, init);
            if (!response.ok) {
                pubsub_1.publish(pubsub_1.SET_APP_STATUS, AppStatus.ERROR, response.status);
                return _createResponse(response);
            }
            return _createResponse(response, await response.json());
        }
        catch (error) {
            pubsub_1.publish(pubsub_1.SET_APP_STATUS, AppStatus.ERROR, error.message);
            throw error;
        }
    };
    const _fetch = async (url, init) => {
        const response = await fetch(url, init);
        if (!response.ok) {
            return _createResponse(response);
        }
        return _createResponse(response, await response.json());
    };
    let _currentSchema;
    let _currentConnection;
    exports.getCurrentSchema = () => _currentSchema;
    exports.getCurrentConnection = () => _currentConnection;
    const getTimezoneHeader = () => {
        return { headers: { "timezone": Intl.DateTimeFormat().resolvedOptions().timeZone } };
    };
    let initialResponse;
    const fetchInitial = async () => _fetchAndPublishStatus("api/initial");
    exports.initializeApi = async () => {
        initialResponse = await fetchInitial();
        pubsub_1.publish(pubsub_1.API_INITIAL, initialResponse);
    };
    exports.connectionIsDefined = connection => {
        if (!initialResponse || !initialResponse.ok || !initialResponse.data || !initialResponse.data.connections || !initialResponse.data.connections.length) {
            return false;
        }
        return initialResponse.data.connections.find(c => c.name == connection) ? true : false;
    };
    exports.fetchConnection = async (name) => {
        const result = await _fetchAndPublishStatus(`api/connection/${name}`, getTimezoneHeader());
        if (!result.data) {
            return result;
        }
        _currentSchema = result.data.schemas.selected;
        _currentConnection = name;
        result.data.connection = name;
        return result;
    };
    exports.fetchSchema = async (schema) => {
        const result = await _fetchAndPublishStatus(`api/schema/${exports.getCurrentConnection()}/${schema}`);
        if (!result.data) {
            return null;
        }
        _currentSchema = result.data.name;
        result.data.connection = exports.getCurrentConnection();
        return result;
    };
    exports.createScript = async () => {
        const result = await _fetch(`api/script-create/${exports.getCurrentConnection()}/${exports.getCurrentSchema()}`);
        if (!result.data) {
            return null;
        }
        result.data.connection = exports.getCurrentConnection();
        result.data.schema = exports.getCurrentSchema();
        return result;
    };
    exports.fetchScriptContent = (connection, id) => _fetch(`api/script-content/${connection}/${id}`);
    exports.saveScriptContent = async (connection, id, content, viewState) => await _fetch(`api/script-content/${connection}/${id}/${encodeURIComponent(viewState)}`, {
        method: 'POST',
        headers: {
            "Accept": "text/plain",
            "Content-Type": "text/plain",
            "_null-content": content === null ? "1" : "",
            "_null-view-state": viewState === null ? "1" : ""
        },
        body: content
    });
    exports.saveScriptScrollPosition = async (connection, id, scrollTop, scrollLeft) => {
        return await _fetch(`api/script-scroll-position/${connection}`, {
            method: 'POST',
            headers: {
                "Accept": "application/json; charset=UTF-8",
                "Content-Type": "application/json; charset=UTF-8",
            },
            body: JSON.stringify({ id, scrollTop, scrollLeft })
        });
    };
});
//# sourceMappingURL=api.js.map