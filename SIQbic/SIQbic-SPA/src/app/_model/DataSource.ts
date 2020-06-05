import { ActionType } from './ActionType.enum';

export class DataSource {
    constructor(id: any, action: ActionType, key: string, value: any) {
        this.id = id;
        this.action = action;
        this.key = key;
        this.value = value;
    }
    
    id: any;

    action: ActionType;

    key: string;

    value: any;

}
