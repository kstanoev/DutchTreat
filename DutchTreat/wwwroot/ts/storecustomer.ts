export class StoreCustomer {
    constructor(private firstName: string, private lastName: string) {

    }

    public showName() {
        alert(this.firstName + " " + this.lastName);
    }
}