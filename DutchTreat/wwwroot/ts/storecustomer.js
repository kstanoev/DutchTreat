"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var StoreCustomer = /** @class */ (function () {
    function StoreCustomer(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
    StoreCustomer.prototype.showName = function () {
        alert(this.firstName + " " + this.lastName);
    };
    return StoreCustomer;
}());
exports.StoreCustomer = StoreCustomer;
//# sourceMappingURL=storecustomer.js.map