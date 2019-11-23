const fs = require("fs");
const {walkSync, rmdirSync, copy} = require("./utils");


rmdirSync("web/libs/monaco-editor");

for (let obj of walkSync("node_modules/monaco-editor/min")) {
    copy(obj, "node_modules/monaco-editor/min", "web/libs/monaco-editor/min");
}
for (let obj of walkSync("node_modules/monaco-editor/min-maps")) {
    copy(obj, "node_modules/monaco-editor/min-maps", "web/libs/monaco-editor/min-maps");
}

var from = "node_modules/monaco-editor/monaco.d.ts";
var to = "web/libs/monaco-editor/monaco.d.ts";
console.log(`install >>> copying ${from} to ${to}`);
fs.copyFileSync(from, to);
