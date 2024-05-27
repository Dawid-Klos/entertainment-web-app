const { createProxyMiddleware } = require("http-proxy-middleware");
const { env } = require("process");

const target = env.ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
  : env.ASPNETCORE_URLS
    ? env.ASPNETCORE_URLS.split(";")[0]
    : "http://localhost:52438";

const context = [
  "/api/auth",
  "/api/auth/login",
  "/api/auth/logout",
  "/api/auth/register",
  "/api/movies",
  "/api/tv-series",
  "/api/trending",
  "/api/bookmarks",
  "/api/user-content/movies",
  "/api/user-content/tv-series",
  "/swagger/v1/swagger.json",
  "/swagger",
];

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target: target,
    secure: false,
    headers: {
      Connection: "Keep-Alive",
    },
  });

  app.use(appProxy);
};
