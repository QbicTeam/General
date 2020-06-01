export class Business {

    business: string;
    server: string;
    db: string;
    userDB: string;
    password: string;
    port: string;
    isDBControl: boolean;
    customerId: number;


    constructor(customerId: number, business: string, isDBControl: boolean, server: string, db: string, userDB: string, password: string, port: string) {
        this.customerId = customerId;
        this.business = business;
        this.isDBControl = isDBControl;
        this.server = server;
        this.db = db;
        this.userDB = userDB;
        this.password = password;
        this.port = port;
    }
}
