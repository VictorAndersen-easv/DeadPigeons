import {PlayerClient} from "@core/generated-ts-client.ts";

const isProduction = import.meta.env.PROD;

 const prod = "https://projectsolutionserver-quiet-rain-8134.fly.dev/";
 const dev = "http://localhost:5284";


export const finalUrl = isProduction ? prod : dev;


export const playerClient = new PlayerClient(finalUrl)

