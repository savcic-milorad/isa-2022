import { IEnvironment } from "./IEnvironment";

export const environment: IEnvironment = {
    production: false,
    // /v1/images/search?breed_ids=beng
    transfusionAPI: new URL("https://api.thecatapi.com") 
};
