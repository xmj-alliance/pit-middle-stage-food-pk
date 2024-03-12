/**
 * @typedef {import('prettier').Options} Options
 */

/** @type {Options} */
module.exports = {
  useTabs: false,
  plugins: [require.resolve("prettier-plugin-apex")],
  overrides: [
    {
      files: "**/lwc/**/*.html",
      options: { parser: "lwc" },
    },
    {
      files: "*.{cmp,page,component}",
      options: { parser: "html" },
    },
  ],
};
