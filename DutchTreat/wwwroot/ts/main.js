"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var storecustomer_1 = require("./storecustomer");
var customer = new storecustomer_1.StoreCustomer("Kiril", "Stanoev");
customer.showName();
function sayHello() {
    var compiler = document.getElementById("compiler").value;
    var framework = document.getElementById("framework").value;
    return "Hello from " + compiler + " and " + framework + "!";
}
//alert(sayHello());
//# sourceMappingURL=main.js.map