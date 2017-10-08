// http://eslint.org/docs/user-guide/configuring

module.exports = {
  root: true,
  parserOptions: {
    sourceType: "module",
    ecmaVersion: 2017,
    parser: "babel-eslint",
    ecmaFeatures: {
      experimentalObjectRestSpread: true,
      spread: true
    }
  },
  env: {
    browser: true,
    es6: true
  },
  // https://github.com/standard/standard/blob/master/docs/RULES-en.md
  extends: 'eslint:recommended',
  // required to lint *.vue files
  plugins: ["html", "vue"],
  // add your custom rules here
  rules: {
    "no-unneeded-ternary": ["error", { "defaultAssignment": false }],
    // allow paren-less arrow functions
    "arrow-parens": 0,
    // allow async-await
    "generator-star-spacing": 0,
    // allow debugger during development
    "no-debugger": process.env.NODE_ENV === "production" ? 2 : 0
  }
};
