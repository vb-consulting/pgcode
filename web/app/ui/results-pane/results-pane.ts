import { ItemInfoType, getConnectionColor, INotice, IExecuteResponse } from "app/api";
import Grid from "app/ui/results-pane/grid";
import Messages from "app/ui/results-pane/messages";

export default class  {
    //private readonly id: string;
    //private readonly data: ItemInfoType;
    private readonly element: Element;
    private readonly tabs: HTMLCollection;
    private readonly panes: HTMLCollection;
    private readonly footerMsg: Element;
    private readonly footerTime: Element;
    private readonly footerRows: Element;
    private readonly undock: ()=>void;
    private readonly grid: Grid;
    private readonly messages: Messages;

    constructor(id: string, element: Element, data: ItemInfoType, undock: ()=>void) {
        //this.id = id;
        //this.data = data;
        this.undock = undock;
        this.element = element.html(String.html`
            <div>
                <div class="tab" id="results" title="results">
                    <i class="icon-database"></i>
                    <span class="title">Results</span>
                    <div class="stripe" style="background-color: ${getConnectionColor(data.connection)}"></div>
                </div>
                <div class="tab" id="messages" title="messages">
                    <i class="icon-database"></i>
                    <span class="title">Messages</span>
                    <div class="stripe" style="background-color: ${getConnectionColor(data.connection)}"></div>
                </div>
            </div>
            <div>
                <div id="results" class="pane"></div>
                <div id="messages" class="pane"></div>
            </div>
            <div>
                <div>
                    <div></div>
                </div>
                <div>
                    <div></div>
                </div>
                <div>
                    <div>-</div>
                </div>
            </div>
        `);

        this.tabs = this.element.children[0].children.on("click", e => this.activateByTab(e.currentTarget as Element));
        this.panes = this.element.children[1].children;

        this.footerMsg = this.element.children[2].children[0].children[0];
        this.footerTime = this.element.children[2].children[1].children[0];
        this.footerRows = this.element.children[2].children[2].children[0];

        this.grid = new Grid(id, this.panes[0]);
        this.messages = new Messages(this.panes[1]);

        this.activateByTab(this.tabs[0]);
    }

    setReady() {
        this.footerMsg.html("🔗 Connected.");
        this.footerTime.html("🕛 --:--:--").css("title", "");
        this.footerRows.html("0 rows").css("title", "");;
    }

    setDisconnected() {
        this.footerMsg.html("⛔ Disconnected.");
        this.footerTime.html("🕛 --:--:--").attr("title", "");
        this.footerRows.html("0 rows").attr("title", "");;
    }

    start() {
        this.undock();
        
        this.footerMsg.html("Running...");
        this.footerTime.html("🕛 --:--:--").attr("title", "");
        this.footerRows.html(" - ");
        //this.error = null;
        this.grid.init();
        this.messages.clear();
    }

    notice(e: INotice) {
        console.log("notice", e);
        this.messages.message(e);
    }

    error(e: INotice) {
        console.log("error", e);
        this.footerMsg.html(`⚠️ ${e.messageText}`);
        this.messages.message(e);
    }

    end(e: IExecuteResponse) {
        console.log("end", e);
        this.messages.finished(e);
        if (e.message != "error") {
            this.footerMsg.html("✔️ Query executed successfully.");
        }
        this.footerTime.html(`🕛 ${e.executionTime}`).attr("title", `total time: ${e.executionTime}`);
        this.footerRows.html(`${e.rowsAffected} rows`);
        this.grid.done(e);
    }

    adjustGrid() {
        this.grid.adjust();
    }

    private activateByTab(tab: Element) {
        for(let current of this.tabs) {
            current.toggleClass("active", tab.id == current.id);
        }
        for(let pane of this.panes) {
            pane.showElement(tab.id == pane.id);
        }
    }
}
