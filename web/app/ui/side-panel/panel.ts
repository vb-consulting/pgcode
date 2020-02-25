import { subscribe, publish, SCHEMA_CHANGED, ITEM_COUNT_CHANGED } from "app/_sys/pubsub";
import { ISchema } from "app/api";
import MonacoContextMenu from "app/controls/monaco-context-menu";
import { ContextMenuCtorArgs, MenuItemType } from "app/controls/context-menu";

class PanelMenu extends MonacoContextMenu {
    protected adjust() {
        this.element.css("top", "0").css("left", "0").visible(false).showElement();
        const target = this.target.getBoundingClientRect();
        const element = this.element.getBoundingClientRect();
        let left: number;
        if (target.left + element.width >= window.innerWidth) {
            left = window.innerWidth - element.width;
        } else {
            left = target.left;
        }
        this.element.css("top", (target.top + target.height) + "px").css("left", left + "px").css("min-width", target.width + "px").visible(true);
    }
}

export default abstract class Panel {
    protected readonly key: string;
    protected readonly element: Element;
    protected readonly header: Element;
    protected readonly items: Element;
    private itemScrollTimeout: number;

    constructor(element: Element, key: string, menuItems: Array<MenuItemType> = []){
        this.element = element;
        this.key = key;
        this.header = element.children[0].html(String.html`
            <div>${key.toUpperCase()}</div>
            <div>
                <span class="btn"><i class="icon-menu"></i></span>
            </div>
        `);
        this.items = element.children[1];

        this.items.on("mouseleave", event => {
            let e = (event.target as Element);
            e.css("overflow-y", "hidden").css("z-index", "");
        }).on("mouseenter", event => {
            let e = (event.target as Element);
            if (e.scrollHeight > e.clientHeight) {
                e.css("overflow-y", "scroll").css("z-index", "1");
            }
        }).on("scroll", () => this.toggleHeaderShadow());
        this.toggleHeaderShadow();

        if (menuItems.length) {
            new PanelMenu({
                id: `${key}-panel-menu`,
                target: this.header.find(".btn"),
                event: "click",
                items: menuItems,
                onOpen: menu => menu.target.addClass("active"),
                onClose: menu => menu.target.removeClass("active")
            } as ContextMenuCtorArgs);

        } else {
            this.header.find(".btn").remove();
        }

        subscribe(SCHEMA_CHANGED, (data: ISchema) => this.schemaChanged(data));
        
        this.items.on("click", e => {
            const element = (e.target as Element).closest("div.panel-item");
            if (!element) {
                return;
            }
            if (!element.hasClass("active")) {
                const active = this.items.findAll(".active");
                if (active.length > 0) {
                    for(let unselect of active) {
                        this.itemUnselected((unselect as Element).removeClass("active"));
                    } 
                }
                element.addClass("active");
                this.itemSelected(element);
            }
        });
    }

    public show(state: boolean) {
        this.element.showElement(state);
    }

    protected createItemElement(content: string) {
        return String.html`
        <div class="panel-item">
            ${content}
        </div>`
        .toElement() as Element
    }

    protected abstract schemaChanged(data: ISchema) : void;

    protected itemSelected(element: Element) : void {};

    protected itemUnselected(element: Element) : void {};

    protected publishLength() {
        publish(ITEM_COUNT_CHANGED, this.key, this.items.children.length);
    }

    private toggleHeaderShadow() {
        if (this.itemScrollTimeout) {
            clearTimeout(this.itemScrollTimeout);
        }
        this.itemScrollTimeout = setTimeout(() => {
            if (this.items.scrollHeight > this.items.clientHeight && this.items.scrollTop) {
                this.header.addClass("shadow");
            } else {
                this.header.removeClass("shadow");
            }
            this.itemScrollTimeout = undefined;
        }, 25);
    }
}