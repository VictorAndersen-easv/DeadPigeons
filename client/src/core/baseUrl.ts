const isProduction = import.meta.env.PROD;

 const prod = "https://projectsolutionserver-quiet-rain-8134.fly.dev/";
 const dev = "http://localhost:5174";
export const finalUrl = isProduction ? prod : dev;