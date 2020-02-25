define(["require", "exports", "app/types", "app/api", "app/ui/side-panel/panel"], function (require, exports, types_1, api_1, panel_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class default_1 extends panel_1.default {
        constructor(element) {
            super(element, types_1.keys.scripts, [
                { text: "New script", keyBindingsInfo: "Ctrl+N", action: () => this.createScript() },
                { splitter: true },
                { text: "Filter" },
                { text: "Order ascending" },
                { text: "Order descending" },
            ]);
        }
        schemaChanged(data) {
            this.items.html("");
            for (let item of data.scripts) {
                this.addNewItem(item);
            }
            this.publishLength();
        }
        async createScript() {
            const response = await api_1.createScript();
            if (response.ok) {
                this.addNewItem(response.data);
                this.publishLength();
            }
        }
        addNewItem(item) {
            const comment = item.comment ? String.html `<div class="item-subtext">${item.comment}</div>` : "";
            this.createItemElement(String.html `
            <div>
                <i class="icon-doc-text"></i>
                <span>${item.title}</span>
            </div>
            <div>
                <div class="item-subtext">${item.timestamp.formatDateString()}</div>
                ${comment}
            </div>
        `)
                .dataAttr("item", item)
                .appendElementTo(this.items);
        }
    }
    exports.default = default_1;
});
//# sourceMappingURL=scripts.js.map