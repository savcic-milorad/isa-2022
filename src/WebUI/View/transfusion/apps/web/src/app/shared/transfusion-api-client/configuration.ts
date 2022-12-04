import { environment } from "apps/web/src/environments/environment";

export interface ConfigurationParameters {
    apiKeys?: {[ key: string ]: string};
    accessToken?: string | (() => string);
}

export class Configuration {
    accessToken?: string | (() => string);
    basePath?: string;

    constructor(configurationParameters: ConfigurationParameters) {
        this.accessToken = configurationParameters.accessToken;
        this.basePath = environment.transfusionApiUrl;
    }
}
