using Minio;
using System.IO;
using Microsoft.AspNetCore.Http;
using Minio.DataModel.Args;
using Minio.DataModel;

public class MinIOUploader
{
    private readonly IMinioClient _minioClient;

    private string endpoint = "localhost:9000";
    private string accessKey = "adkBQ7HSGAS34lJozZyj";
    private string secretKey = "DU9xjAdodw409joJUnw6A5tbjBsEeU8mbIfXgDWK";

    private string bucketName = "paperless";
    private string objectName = "paperlessObject";

    public MinIOUploader()
    {
        _minioClient = new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();
                        
    }

    public async Task UploadFileAsync(IFormFile file)
    {

        // Upload the file to MinIO
        using (var stream = file.OpenReadStream())
        {
            PutObjectArgs poa = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
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
                .WithObject(objectName);

            ObjectStat objectStat = await _minioClient.StatObjectAsync(statOjbectArgs);
            Console.WriteLine(objectStat.ObjectName, objectStat.ETag);
        }
    }
}
