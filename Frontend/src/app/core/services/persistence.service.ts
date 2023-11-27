import {Injectable} from "@angular/core";


@Injectable()

export class PersistenceService {
    set(key: string, data: any): void {
        try {
            localStorage.setItem(key, JSON.stringify(data))
        } catch (e) {
            console.error('Error saving to localStore', e)
        }
    }


    get(key: string): any {
        try {
            const item = localStorage.getItem(key);
            if (item !== null) {
                return JSON.parse(item);
            }
        } catch (e) {
            console.error('Error getting data from localStorage', e)
            return null
        }
    }

}

