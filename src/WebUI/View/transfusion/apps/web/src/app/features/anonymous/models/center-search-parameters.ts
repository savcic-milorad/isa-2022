

export class CenterSearchParameters {

    constructor(
        private readonly Name: string, 
        private readonly City: string,
        private readonly State: string
    ) {}

    static Initial() {
        return new CenterSearchParameters('', '', '');
    }
}