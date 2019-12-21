const express = require("express");
const serveStatic = require("serve-static");
const path = require("path");

const app = express();

app.use("/", serveStatic(path.join(__dirname, "/dist")));

app.get(/.*/, function(req, res) {
  res.sendFile(path.join(__dirname, "/dist/index.html"));
});

app.get("/.well-known/acme-challenge/:content", function(req, res) {
  res.send("xxxxxxxxxxxx-yyyy.zzzzzzzzzzzzzzzzzzz");
});

const port = process.env.PORT || 8080;
app.listen(port);
console.log(`app is listening on port: ${port}`);
