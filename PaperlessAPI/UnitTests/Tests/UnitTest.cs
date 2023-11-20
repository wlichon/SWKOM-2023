using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NPaperless.Services;
using NPaperless.Services.Data;
using NPaperless.Services.Models;
using NPaperless.Services.Repositories.DocumentsRepos;
using System.Data.Entity.Infrastructure;
using System.Reflection.Metadata;
using Document = NPaperless.Services.Models.Document;

namespace NPaperless.DataAccess.Tests
{
    [TestFixture]
    public class MappingTests
    {
        IMapper _mapper;
        MapperConfiguration _config;

        [SetUp]
        public void Setup()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = _config.CreateMapper();
        }

        [Test]

        public void ConfigurationTest()
        {
            _config.AssertConfigurationIsValid();

            Assert.Pass();
        }

        [Test]

        public void DocumentMapperTest()
        {
            var docDto = new DocumentDto
            {
                Correspondent = 2,
                DocumentType = 1,
                Title = "Document",
                Tags = new uint[] { 1, 2, 3 },
                Created = DateTime.MinValue,
                Added = DateTime.MaxValue
            };

            var doc = _mapper.Map<Document>(docDto);

            Assert.IsNotNull(doc);

            Assert.That(doc.Correspondent == docDto.Correspondent);
            Assert.That(doc.DocumentType == docDto.DocumentType);
            Assert.That(doc.Title == docDto.Title);
            Assert.That(doc.Tags.SequenceEqual(docDto.Tags));
            Assert.That(doc.Created == docDto.Created);
            Assert.That(doc.Added == docDto.Added);

        }


        [Test]

        public void DocumentMapperTestReverse()
        {
            var doc = new Document
            {
                Correspondent = 2,
                DocumentType = 1,
                Title = "Document",
                Tags = new uint[] { 1, 2, 3 },
                Created = DateTime.MinValue,
                Added = DateTime.MaxValue
            };

            var docDto = _mapper.Map<DocumentDto>(doc);

            Assert.IsNotNull(doc);

            Assert.That(doc.Correspondent == docDto.Correspondent);
            Assert.That(doc.DocumentType == docDto.DocumentType);
            Assert.That(doc.Title == docDto.Title);
            Assert.That(doc.Tags.SequenceEqual(docDto.Tags));
            Assert.That(doc.Created == docDto.Created);
            Assert.That(doc.Added == docDto.Added);

        }
    }

    [TestFixture]

    public class DocumentDbTests
    {

        IMapper _mapper;
        MapperConfiguration _config;

        [SetUp]

        public void Setup()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = _config.CreateMapper();



        }

        [Test]

        public void CreateOneDocument()
        {
            var mockSet = new Mock<DbSet<Document>>();

            var mockContext = new Mock<DataContext>();

            mockContext.Setup(m => m.Documents).Returns(mockSet.Object);

            var _docRepo = new DocumentRepo(mockContext.Object, _mapper);

            var docDto = new DocumentDto
            {
                Correspondent = 2,
                DocumentType = 1,
                Title = "Document",
                Tags = new uint[] { 1, 2, 3 },
                Created = DateTime.MinValue,
                Added = DateTime.MaxValue
            };

            var doc = _docRepo.CreateOneDoc(docDto);

            mockSet.Verify(m => m.Add(It.IsAny<Document>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        }


        [Test]

        public async System.Threading.Tasks.Task GetAllDocuments()
        {
            var data = new List<Document>()
            {
                new Document {
                    Correspondent = 2,
                    DocumentType = 1,
                    Title = "Document1",
                    Tags = new uint[] { 1, 2, 3 },
                    Created = DateTime.MinValue,
                    Added = DateTime.MaxValue
                },
                new Document
                {
                    Correspondent = 3,
                    DocumentType = 2,
                    Title = "Document2",
                    Tags = new uint[] { 4, 5, 6 },
                    Created = DateTime.MinValue,
                    Added = DateTime.MaxValue
                },
                new Document
                {
                    Correspondent = 1,
                    DocumentType = 4,
                    Title = "Document3",
                    Tags = new uint[] { 7, 8, 9 },
                    Created = DateTime.MinValue,
                    Added = DateTime.MaxValue
                }
            }.AsQueryable();


            var mockSet = new Mock<DbSet<Document>>();
            mockSet.As<IDbAsyncEnumerable<Document>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Document>(data.GetEnumerator()));

            mockSet.As<IQueryable<Document>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Document>(data.Provider));
            mockSet.As<IQueryable<Document>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Document>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Document>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockContext = new Mock<DataContext>();

            mockContext.Setup(m => m.Documents).Returns(mockSet.Object);

            var _docRepo = new DocumentRepo(mockContext.Object, _mapper);



            var docs = await _docRepo.GetAllDocs();

            Assert.That(docs.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(data.ToList(), docs.ToList());


        }
    }


}