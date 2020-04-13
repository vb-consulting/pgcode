import Storage from "app/_sys/storage";
import { subscribe, publish, SPLITTER_CHANGED, TAB_SELECTED, TAB_UNSELECTED, SCHEMA_CHANGED } from "app/_sys/pubsub";
import createTabElement from "app/ui/main-panel/tabs";
import Content from "app/ui/main-panel/content";
import {ItemInfoType, Keys, ISchema, classes, IScriptContent, ItemContentArgs} from "app/api";
import timeout from "app/_sys/timeout";

interface Item extends IStorageItem {
    tab: Element
}

interface IStorageItem {
    id: string,
    key: Keys,
    timestamp: number,
    data: ItemInfoType
}

interface IStorage {
    stickyId: string,
    activeId: string,
    items: Array<[string, IStorageItem]>
}

const 
    _storage = new Storage({
            stickyId: null,
            activeId: null,
            items: []
        } as IStorage, 
        "tabs", 
        (name, value) => name === "items" ? JSON.parse(value) : value,
        (name, value) => name === "items" ? JSON.stringify(value) : value
    ) as any as IStorage;

const 
    _updateStorageTabItems: (items: Map<string, Item>) => void = items => setTimeout(() => 
        _storage.items = Array.from<[string, Item], [string, IStorageItem]>(items.entries(), (v: [string, Item], k: number) => {
            return [v[0], { id: v[1].id, key: v[1].key, timestamp: v[1].timestamp, data: v[1].data } as IStorageItem];
        }));

export default class  {
    private element: Element;
    private readonly tabs: Element;
    private content: Content;
    private headerHeight: number;
    private headerRows: number = 1;
    private activeTab: Element;
    private stickyTab: Element;
    private items: Map<string, Item> = new Map<string, Item>();

    constructor(element: Element){
        this.element = element.addClass("main-panel").html(
            String.html`
                <div></div>
                <div></div>
            `);
        this.tabs = element.children[0];
        this.content = new Content(element.children[1]);
        this.initHeaderAdjustment();

        this.restoreItems();
        subscribe(SCHEMA_CHANGED, (data: ISchema, name: string) => this.schemaChanged(name, data.connection));
    }

    public unstickById(id: string) {
        if (this.stickyTab && this.stickyTab.id == id) {
            this.stickyTab.removeClass(classes.sticky);
            this.stickyTab = null;
            _storage.stickyId = null;
        }
    }

    public activate(id: string, key: Keys, data: ItemInfoType, contentArgs = ItemContentArgs) {
        const item = this.items.get(id);
        if (item) {
            // tab already exists
            this.activateByTab(item.tab);

        } else {
            // create a new tab
            const tab = this.createNew(id, key, data, contentArgs.content);
            if (contentArgs.sticky) {
                if (this.stickyTab) {
                    this.items.delete(this.stickyTab.id);
                    _storage.stickyId = null;
                    this.stickyTab.replaceWith(this.makeStickyTab(tab));
                } else {
                    this.makeStickyTab(tab).appendElementTo(this.tabs);
                }
            } else {
                tab.appendElementTo(this.tabs);
            }
            this.items.set(id, {tab, id, key, data} as Item);
            this.activateByTab(tab, item);
        }
        _updateStorageTabItems(this.items);
    }

    private restoreItems() {
        const stickyId = _storage.stickyId;
        const activeId = _storage.activeId;
        for(let [id, storageItem] of _storage.items) {
            const tab = this.createNew(id, storageItem.key, storageItem.data).appendElementTo(this.tabs);
            if (id === stickyId) {
                this.stickyTab = tab.addClass(classes.sticky);
            }
            if (id === activeId) {
                this.activeTab = tab.addClass(classes.active);
            }
            this.items.set(id, {tab, ...storageItem} as Item);
        }
        if (this.activeTab) {
            this.activated(this.activeTab.id);
            this.initiateHeaderAdjust();
        }
    }

    private schemaChanged(schema: string, connection: string) {
        if (!this.activeTab) {
            return;
        }
        const item = this.items.get(this.activeTab.id);
        if (!item) {
            return;
        }
        if (item.data.schema !== schema || item.data.connection !== connection) {
            return;
        }
        setTimeout(() => publish(TAB_SELECTED, item.id, item.key, item.data.schema, item.data.connection));
    }

    private activateByTab(tab: Element, item?: Item) {
        for(let t of this.tabs.children) {
            if (t.hasClass(classes.active)) {
                t.removeClass(classes.active);
                let remove = this.items.get(t.id);
                _storage.activeId = null;
                publish(TAB_UNSELECTED, remove.id, remove.key);
            }
        }
        this.activeTab = tab.addClass(classes.active);
        _storage.activeId = tab.id;
        this.activated(tab.id, item);
        this.initiateHeaderAdjust();
    }

    private activated(id: string, item?: Item) {
        if (!item) {
            item = this.items.get(id);
        }
        item.timestamp = new Date().getTime();
        this.content.activate(id);
        publish(TAB_SELECTED, item.id, item.key, item.data.schema, item.data.connection);
    }

    private removeByTab(tab: Element) {
        const 
            id = tab.id, 
            active = tab.hasClass(classes.active), 
            sticky = tab.hasClass(classes.sticky), 
            item = this.items.get(id);
        this.items.delete(id);
        tab.remove();
        this.content.remove(id);
        if (sticky) {
            this.stickyTab = null;
            _storage.stickyId = null;
        }
        if (!active) {
            return;
        }
        _storage.activeId = null;
        publish(TAB_UNSELECTED, item.id, item.key);
        if (!this.items.size) {
            return;
        }
        let newItem = this.items.maxBy(v => v.timestamp);
        this.activateByTab(newItem.tab, newItem);
    }

    private createNew(id: string, key: Keys, data: ItemInfoType, content: IScriptContent = null) {
        this.content.createNew(id, key, data, content);
        return createTabElement(id, key, data)
            .on("click", e => this.tabClick(e))
            .on("dblclick", e => this.tabDblClick(e));
    }

    private makeStickyTab(tab: Element) {
        this.stickyTab = tab;
        _storage.stickyId = tab.id;
        return tab.addClass(classes.sticky);
    }

    private tabClick(e: Event) {
        const target = e.target as Element;
        const currentTarget = e.currentTarget as Element;
        if (target.hasClass("close")) {
            this.removeByTab(currentTarget);
            _updateStorageTabItems(this.items);
            return;
        }
        this.activateByTab(currentTarget);
        _updateStorageTabItems(this.items);
    }

    private tabDblClick(e: Event) {
        const tab = e.currentTarget as Element;
        if (tab.hasClass(classes.sticky)) {
            tab.removeClass(classes.sticky);
            this.stickyTab = null;
        }
    }
    
    private initHeaderAdjustment() {
        this.headerHeight = Number(this.element.css("grid-template-rows").split(" ")[0].replace("px", ""));
        window.on("resize", () => this.initiateHeaderAdjust());
        subscribe(SPLITTER_CHANGED, () => this.initiateHeaderAdjust());
    }

    private initiateHeaderAdjust() {
        timeout(() => this.adjustHeaderHeight(), 10, "main-panel-adjust");
    }

    private adjustHeaderHeight() {
        let lastTop: number;
        let rows: number = 1;
        for(let t of this.tabs.children) {
            let top = t.getBoundingClientRect().top;
            if (lastTop != undefined && lastTop < top) {
                rows++;
            }
            lastTop = top;
            t.dataAttr("row", rows);
        }
        if (this.activeTab) {
            if (this.activeTab.dataAttr("row") as number != rows) {
                this.activeTab.addClass("upper-row");
            } else {
                this.activeTab.removeClass("upper-row");
            }
        }
        if (rows != this.headerRows) {
            this.element.css("grid-template-rows", `${rows * this.headerHeight}px auto`);
            this.headerRows = rows;
        }
    }
}
