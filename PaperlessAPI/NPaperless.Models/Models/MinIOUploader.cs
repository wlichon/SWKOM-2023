using Minio;
using System.IO;
using Microsoft.AspNetCore.Http;
using Minio.DataModel.Args;
using Minio.DataModel;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

public class MinIOUploader
{
    private readonly IMinioClient _minioClient;

    private readonly IConfiguration _config;

    private string endpoint;
    private string accessKey = "adkBQ7HSGAS34lJozZyj";
    private string secretKey = "DU9xjAdodw409joJUnw6A5tbjBsEeU8mbIfXgDWK";

    private string bucketName = "paperless";

    public MinIOUploader(IHostingEnvironment env)
    {
        string environment = env.EnvironmentName;

        if(environment == "Development")
        {
            endpoint = "localhost:9000";
        }
        else
        {
            endpoint = "minio:9000";
        }
        

        _minioClient = new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();
                        
    }

    public async Task UploadFileAsync(IFormFile file)
    {
        string objName = file.FileName;
        // Upload the file to MinIO
        using (var stream = file.OpenReadStream())
        {
            PutObjectArgs poa = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(file.ContentType);
            try
            {
                await _minioClient.PutObjectAsync(poa);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            StatObjectArgs statOjbectArgs = new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objName);

            ObjectStat objectStat = await _minioClient.StatObjectAsync(statOjbectArgs);
            Console.WriteLine(objectStat.ObjectName, objectStat.ETag);
        }
    }
}
