using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using System.IO;

namespace PetFamily.Infrastructure.Services;

public class MinioProvider : IMinioProvider
{
    private const string PhotoBucket = "images";

    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<string, ResultEvent>> UploadPhoto(IFormFile photo, string path, CancellationToken ct)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(PhotoBucket);

            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(PhotoBucket);

                await _minioClient.MakeBucketAsync(makeBucketArgs, ct);
            }

            await using (var stream = photo.OpenReadStream())
            {
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(PhotoBucket)
                    .WithStreamData(stream)
                    .WithObjectSize(stream.Length)
                    .WithObject(path);

                var response = await _minioClient.PutObjectAsync(putObjectArgs, ct);

                return response.ObjectName;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Errors.General.SaveFailure("photo");
        }
    }

    public async Task<Result<ResultEvent>> RemovePhoto(string path, CancellationToken ct)
    {
        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(PhotoBucket)
                .WithObject(path);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, ct);

            return Seccess.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Errors.General.DeleteFailure("photo");
        }
    }

    public async Task<Result<string, ResultEvent>> GetPhoto(string path)
    {
        try
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
           .WithBucket("images")
           .WithObject(path)
           .WithExpiry(60 * 60 * 24);

            var url = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

            return url;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Errors.General.GetFailure("photo");
        }
    }

    public async Task<Result<IReadOnlyList<string>, ResultEvent>> GetPhotos(List<string> paths, CancellationToken ct)
    {
        try
        {
            List<string> urls = [];
            foreach (string path in paths)
            {
                var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket("images")
                .WithObject(path)
                .WithExpiry(60 * 60 * 24);

                var url = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

                urls.Add(url);
            }

            return urls;
            //сделал ужасно не красиво, но пока сойдет
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Errors.General.GetFailure("photo");
        }
    }
}