define(["require", "exports", "app/ui/content/editor", "app/controls/splitter", "app/api", "app/_sys/storage", "app/_sys/pubsub", "app/ui/results-pane/results-pane"], function (require, exports, editor_1, splitter_1, api_1, storage_1, pubsub_1, results_pane_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    const _defaultSplitValue = { height: 250, docked: true };
    const _storage = new storage_1.default({ splitter: {} }, "content", (name, value) => JSON.parse(value), (name, value) => JSON.stringify(value)), _getSplitterVal = id => {
        const s = _storage.splitter, v = s[id];
        if (!v) {
            return _defaultSplitValue;
        }
        return v;
    }, _setSplitterVal = (id, item) => {
        const s = _storage.splitter, v = s[id];
        s[id] = { ...(v ? v : _defaultSplitValue), ...item };
        _storage.splitter = s;
    };
    class Content {
        constructor(element) {
            this.container = element;
            Content.instance = this;
        }
        disposeEditor(id) {
            const e = this.getContentElement(id);
            if (!e.length) {
                return;
            }
            this.editor(e).dispose();
        }
        async createOrActivateContent(id, key, data, contentArgs = { content: null, sticky: false }) {
            if (contentArgs.sticky) {
                if (this.stickyId && id != this.stickyId) {
                    const e = this.getContentElement(this.stickyId);
                    if (e.length) {
                        this.editor(e).dispose();
                        e.remove();
                    }
                }
                this.stickyId = id;
            }
            await this.createNewContent(id, key, data, contentArgs);
        }
        async createNewContent(id, key, data, contentArgs = { content: null, sticky: false }) {
            if (contentArgs.sticky) {
                this.stickyId = id;
            }
            const newElement = this.createElement(id, key, contentArgs.content, data)
                .hideElement()
                .attr("id", id)
                .dataAttr("key", key)
                .addClass("content")
                .appendElementTo(this.container);
            if (!contentArgs.content && key === api_1.Keys.SCRIPTS) {
                const response = await api_1.fetchScriptContent(data.connection, data.id);
                if (response.ok) {
                    this.editor(newElement).setContent(response.data);
                }
            }
        }
        setStickStatus(id, value) {
            if (value) {
                this.stickyId = id;
            }
            else {
                this.stickyId = undefined;
            }
        }
        activate(id) {
            const e = this.getContentElement(id);
            if (!e.length) {
                return false;
            }
            if (this.active) {
                this.active.hideElement().removeClass(api_1.classes.active);
            }
            this.active = e.showElement().addClass(api_1.classes.active);
            setTimeout(() => this.editor(e).layout().focus());
            pubsub_1.publish(pubsub_1.CONTENT_ACTIVATED, e.dataAttr("data").name);
            return true;
        }
        getContent(id) {
            const e = this.getContentElement(id);
            if (!e.length) {
                return null;
            }
            const editor = e.dataAttr("editor");
            if (editor) {
                return editor.getContent();
            }
            return e.html().trim();
        }
        remove(id) {
            const e = this.getContentElement(id);
            if (!e.length) {
                return;
            }
            this.editor(e).dispose();
            e.remove();
            if (this.container.children.length == 0) {
                pubsub_1.publish(pubsub_1.CONTENT_ACTIVATED, null);
            }
        }
        actionRun(id) {
            this.editor(this.active).actionRun(id);
        }
        getAllContent() {
            const result = new Array();
            for (let e of this.container.children) {
                result.push({ id: e.id, key: e.dataAttr("key"), data: e.dataAttr("data"), active: e.hasClass(api_1.classes.active) });
            }
            return result;
        }
        createElement(id, key, content, data) {
            if (key == api_1.Keys.SCRIPTS) {
                return this.createSplitEditor(id, api_1.Languages.PGSQL, content, data);
            }
            return String.html `
            <div>
                ${key.toString()}:  ${data.id}
            </div>`
                .toElement()
                .dataAttr("data", data);
        }
        createSplitEditor(id, lang, content, data) {
            const element = String.html `
            <div>
                <div class="editor"></div>
                <div></div>
                <div class="results-pane"></div>
            </div>`
                .toElement()
                .addClass("split-content")
                .css("grid-template-rows", `auto 5px ${_getSplitterVal(id).height}px`)
                .dataAttr("data", data);
            let splitter;
            const results = new results_pane_1.default(id, element.children[2], data, () => splitter.undock());
            element.dataAttr("results", results);
            const editor = new editor_1.Editor(id, element.children[0], element, lang, content, results);
            element.dataAttr("editor", editor);
            splitter = new splitter_1.HorizontalSplitter({
                element: element.children[1],
                container: element,
                resizeIndex: 2,
                maxDelta: 100,
                min: 25,
                events: {
                    docked: () => {
                        editor.layout();
                        results.adjustGrid();
                    },
                    undocked: () => {
                        editor.layout();
                        results.adjustGrid();
                    },
                    changed: () => {
                        editor.layout();
                        results.adjustGrid();
                    }
                },
                storage: {
                    get position() {
                        return _getSplitterVal(id).height;
                    },
                    set position(value) {
                        _setSplitterVal(id, { height: value });
                    },
                    get docked() {
                        return _getSplitterVal(id).docked;
                    },
                    set docked(value) {
                        _setSplitterVal(id, { docked: value });
                    }
                }
            }).start();
            return element;
        }
        editor(e) {
            const editor = e.dataAttr("editor");
            if (editor) {
                return editor;
            }
            return editor_1.nullEditor;
        }
        getContentElement(id) {
            return this.container.find("#" + id);
        }
    }
    exports.default = Content;
});
//# sourceMappingURL=content.js.map