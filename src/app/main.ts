///<reference path="../libs/ihjs/types/core.d.ts"/>
///<reference path="../libs/monaco-editor/monaco.d.ts"/>

import "vs/editor/editor.main";

export default class {
    constructor(element: HTMLElement){
        element.html("main area");

        monaco.editor.create(element, {
            value: "",
            language: "pgsql",
            theme: "vs-dark",
            renderWhitespace: "all",
            automaticLayout: false
        });

    }
}