import {AuthClient} from "@core/generated-ts-client.ts";
import {finalUrl} from "@core/baseUrl.ts";
import {customFetch} from "@utilities/customFetch.ts";


export const authApi = new AuthClient(finalUrl, customFetch);