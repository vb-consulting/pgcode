define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class default_1 {
        constructor(id, element) {
            this.moving = false;
            this.id = id;
            this.element = element;
            this.table = null;
        }
        initGrid() {
            this.element.html("");
            this.table = document.createElement("div").appendElementTo(this.element).addClass("table");
            this.last = null;
            this.first = null;
            window
                .on("mousedown", (e) => this.mousedown(e))
                .on("mouseup", (e) => this.mouseup(e))
                .on("mousemove", (e) => this.mousemove(e))
                .on("resize", () => this.adjustGrid());
        }
        addHeader(header) {
            const th = document.createElement("div").appendElementTo(this.table).addClass("th").dataAttr("rn", 0);
            document.createElement("div").appendElementTo(th).addClass("td");
            let i = 1;
            for (let item of header) {
                document
                    .createElement("div")
                    .html(`<div>${item.name}</div><div>${item.type}</div>`)
                    .appendElementTo(th)
                    .addClass("td")
                    .dataAttr("i", i++)
                    .on("mousemove", (e) => this.cellMousemove(e));
            }
        }
        addRow(rn, row) {
            const tr = document.createElement("div").appendElementTo(this.table).addClass("tr").dataAttr("rn", rn);
            if (!this.first) {
                this.first = tr;
            }
            this.last = tr;
            document.createElement("div").html(`${rn}`).appendElementTo(tr).addClass("td").addClass("th");
            let i = 1;
            for (let item of row) {
                let td = document
                    .createElement("div")
                    .html(`${(item == null ? "NULL" : item)}`)
                    .appendElementTo(tr)
                    .addClass("td")
                    .addClass(`td${i++}`);
                if (item == null) {
                    td.addClass("null");
                }
            }
        }
        adjustGrid() {
            if (!this.table) {
                return;
            }
            const rect = this.element.parentElement.getBoundingClientRect();
            this.table.css("height", rect.height + "px");
            if (this.last == null || this.first == null) {
                return;
            }
            const last = this.last.getBoundingClientRect();
            const first = this.first.getBoundingClientRect();
            if (first.y < rect.y || last.y > (rect.y + rect.height)) {
                this.table.css("overflow-y", "scroll");
            }
            else {
                this.table.css("overflow-y", "hidden");
            }
            if (first.width > rect.width) {
                this.table.css("overflow-x", "scroll");
            }
            else {
                this.table.css("overflow-x", "hidden");
            }
        }
        cellMousemove(e) {
            if (this.moving) {
                return;
            }
            const cell = e.currentTarget;
            const rect = cell.getBoundingClientRect();
            if (e.offsetX < 2) {
                document.body.css("cursor", "col-resize");
                this.toMove = cell.previousElementSibling;
            }
            else if (e.offsetX > rect.width - 4) {
                document.body.css("cursor", "col-resize");
                this.toMove = cell;
            }
            else {
                document.body.css("cursor", "default");
                this.toMove = null;
            }
        }
        mousedown(e) {
            if (this.toMove) {
                this.moving = true;
                document.body.css("cursor", "col-resize");
                this.toMove.css("border-right-style", "dotted");
                if (this.toMove.nextElementSibling) {
                    this.toMove.nextElementSibling.css("border-left-style", "dotted");
                }
                this.table.findAll(`div.tr > div.td${this.toMove.dataAttr("i")}`).css("border-right-style", "dotted");
                this.table.findAll(`div.tr > div.td${this.toMove.dataAttr("i") + 1}`).css("border-left-style", "dotted");
            }
        }
        mouseup(e) {
            this.moving = false;
            document.body.css("cursor", "default");
            if (!this.toMove) {
                return;
            }
            this.toMove.css("border-right-style", "");
            if (this.toMove.nextElementSibling) {
                this.toMove.nextElementSibling.css("border-left-style", "");
            }
            this.table.findAll(`div.tr > div.td${this.toMove.dataAttr("i")}`).css("border-right-style", "");
            this.table.findAll(`div.tr > div.td${this.toMove.dataAttr("i") + 1}`).css("border-left-style", "");
            this.toMove = null;
        }
        mousemove(e) {
            if (!this.moving) {
                if (!e.target.hasClass("th") && !e.target.parentElement.hasClass("th") && !e.target.parentElement.parentElement.hasClass("th")) {
                    document.body.css("cursor", "default");
                }
                return;
            }
            const rect = this.toMove.getBoundingClientRect();
            const w = (e.clientX - rect.x - 11) + "px";
            this.toMove.css("min-width", w).css("max-width", w);
            this.table.findAll(`div.tr > div.td${this.toMove.dataAttr("i")}`).css("min-width", w).css("max-width", w);
        }
    }
    exports.default = default_1;
});
//# sourceMappingURL=results.js.map