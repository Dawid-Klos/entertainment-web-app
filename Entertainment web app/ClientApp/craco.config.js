const TsconfigPathsPlugin = require("tsconfig-paths-webpack-plugin");

module.exports = {
  plugins: [
    {
      plugin: {
        overrideWebpackConfig: ({ webpackConfig }) => {
          console.log(webpackConfig);
          webpackConfig.resolve.plugins.push(new TsconfigPathsPlugin({}));
          return webpackConfig;
        },
      },
    },
  ],
};
