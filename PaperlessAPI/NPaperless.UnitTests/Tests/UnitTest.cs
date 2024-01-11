using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
                correspondent = 2,
                documentType = 1,
                title = "Document",
                tags = new uint[] { 1, 2, 3 },
                created = DateTime.MinValue,
                added = DateTime.MaxValue
            };

            var doc = _mapper.Map<Document>(docDto);

            Assert.IsNotNull(doc);

            Assert.That(doc.correspondent == docDto.correspondent);
            Assert.That(doc.documentType == docDto.documentType);
            Assert.That(doc.title == docDto.title);
            Assert.That(doc.tags.SequenceEqual(docDto.tags));
            Assert.That(doc.created == docDto.created);
            Assert.That(doc.added == docDto.added);

        }


        [Test]

        public void DocumentMapperTestReverse()
        {
            var doc = new Document
            {
                correspondent = 2,
                documentType = 1,
                title = "Document",
                tags = new uint[] { 1, 2, 3 },
                created = DateTime.MinValue,
                added = DateTime.MaxValue
            };

            var docDto = _mapper.Map<DocumentDto>(doc);

            Assert.IsNotNull(doc);

            Assert.That(doc.correspondent == docDto.correspondent);
            Assert.That(doc.documentType == docDto.documentType);
            Assert.That(doc.title == docDto.title);
            Assert.That(doc.tags.SequenceEqual(docDto.tags));
            Assert.That(doc.created == docDto.created);
            Assert.That(doc.added == docDto.added);

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
                correspondent = 2,
                documentType = 1,
                title = "Document",
                tags = new uint[] { 1, 2, 3 },
                created = DateTime.MinValue,
                added = DateTime.MaxValue
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
                    correspondent = 2,
                    documentType = 1,
                    title = "Document1",
                    tags = new uint[] { 1, 2, 3 },
                    created = DateTime.MinValue,
                    added = DateTime.MaxValue
                },
                new Document
                {
                    correspondent = 3,
                    documentType = 2,
                    title = "Document2",
                    tags = new uint[] { 4, 5, 6 },
                    created = DateTime.MinValue,
                    added = DateTime.MaxValue
                },
                new Document
                {
                    correspondent = 1,
                    documentType = 4,
                    title = "Document3",
                    tags = new uint[] { 7, 8, 9 },
                    created = DateTime.MinValue,
                    added = DateTime.MaxValue
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