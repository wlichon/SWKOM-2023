  docker run --name openapi-gen -v ${PWD}:/local openapitools/openapi-generator-cli generate -i /local/swagger.json -g aspnetcore -p aspnetCoreVersion=6.0 -p pocoModels=true -p useSeperateModelProject=true --package-name NPaperless.Services --model-package NPaperless.Services.DTOs -o /tmp/out/
  
  docker cp openapi-gen:/tmp/out/ ./out/

  docker rm -f openapi-gen