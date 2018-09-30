import { StoreCustomer } from "./storecustomer";

let customer = new StoreCustomer("Kiril", "Stanoev");
customer.showName();

function sayHello() {
    const compiler = (document.getElementById("compiler") as HTMLInputElement).value;
    const framework = (document.getElementById("framework") as HTMLInputElement).value;
    return `Hello from ${compiler} and ${framework}!`;
}

//alert(sayHello());