import { Keys } from "app/types";
import { ISchema, IScriptInfo, createScript, ScriptId } from "app/api";
import Panel from "app/ui/side-panel/panel"

export default class extends Panel {
    constructor(element: Element) {
        super(element, Keys.SCRIPTS, [
            {text: "New script", keyBindingsInfo: "Ctrl+N", action: () => this.createScript()},
            {splitter: true},
            {text: "Filter"},
            {text: "Order ascending"},
            {text: "Order descending"},
        ]);
    }

    protected schemaChanged(data: ISchema) {
        this.items.html("");
        for(let item of data.scripts) {
            this.addNewItem(item);
        }
        this.publishLength();
    }

    private async createScript() {
        const response = await createScript();
        if (response.ok) {
            this.addNewItem(response.data as IScriptInfo);
            this.publishLength();
            //this.mainPanel activate with content
        }
    }

    private addNewItem(item: IScriptInfo) {
        let title = `${item.title}\nmodified: ${item.timestamp}`;
        if (item.comment) {
            title = title + `\n\n${item.comment.substring(0,200)}`;
        }
        this.createItemElement(String.html`
            <div>
                <i class="icon-doc-text"></i>
                <span>${item.title}</span>
            </div>
            <div>
                <div class="item-subtext">${item.timestamp.formatDateString()}</div>
            </div>
        `)
        .dataAttr("item", item)
        .attr("title", title)
        .attr("id", ScriptId(item.id))
        .appendElementTo(this.items);
    }

    protected itemSelected(element: Element) {
        this.mainPanel.activateScript(element.dataAttr("item") as IScriptInfo);
    };
}