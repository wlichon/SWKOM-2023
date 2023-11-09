# nPaperlessREST

## OpenAPI generated server

## Setup Pre-Requisites

* Install Docker & Docker-Compose

## HOWTO

### Steps to reproduce:

1. Start just with openapi-gen.sh and swagger.json
2. Run ```./openapi-gen.ps1```
3. The generated soltiuon will be in ```out/``` - open with VisualStudioCode and try out if source was generated as expected.
4. move all files from the ```out/``` directory to the current working directory, e.g.
```mv -rf out/* .```
5. Start VisualStudioCode again and open the project in the current working directory
6. Run the server
7. Open the browser in http://localhost:8080/

### Next steps:

* create fake implementations in ApiApiController to try out the REST-Server