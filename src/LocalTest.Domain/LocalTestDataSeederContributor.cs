using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using EasyAbp.Abp.Trees;
using LocalTest.Books;
using LocalTest.Families;
using LocalTest.FamilyTrees;
using LocalTest.FamilyLibs;
using LocalTest.MaterialSpecificationList;
using LocalTest.FactoryList;

namespace LocalTest
{
    public class LocalTestDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Family, Guid> _familyRepository;

        private readonly IRepository<Book, Guid> _bookRepository;

        private readonly ITreeRepository<FamilyTree> _familyTreeRepository;

        private readonly ITreeRepository<FamilyLib> _familyLibRepository;

        private readonly IRepository<MaterialSpecification, Guid> _materialSpecificationRepository;

        private readonly IRepository<MaterialSpecificationDetail, Guid> _materialSpecificationDetailRepository;

        private readonly IRepository<RequisitionList, Guid> _requisitionRepository;

        public LocalTestDataSeederContributor(IRepository<Book, Guid> bookRepository,
            IRepository<Family, Guid> familyRepository,
            ITreeRepository<FamilyTree> treeRepository,
            ITreeRepository<FamilyLib> familyLibRepository,
            IRepository<MaterialSpecification, Guid> materialSpecificationRepository,
            IRepository<MaterialSpecificationDetail, Guid> materialSpecificationDetailRepository,
                 IRepository<RequisitionList, Guid> requistionRepository
            )
        {
            _bookRepository = bookRepository;
            _familyRepository = familyRepository;
            _familyTreeRepository = treeRepository;
            _familyLibRepository = familyLibRepository;
            _materialSpecificationRepository = materialSpecificationRepository;
            _materialSpecificationDetailRepository = materialSpecificationDetailRepository;
            _requisitionRepository = requistionRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() <= 0)
            {
                await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "1984",
                        Type = BookType.Dystopia,
                        PublishDate = new DateTime(1949, 6, 8),
                        Price = 19.84f
                    },
                    autoSave: true
                );

                await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "The Hitchhiker's Guide to the Galaxy",
                        Type = BookType.ScienceFiction,
                        PublishDate = new DateTime(1995, 9, 27),
                        Price = 42.0f
                    },
                    autoSave: true
                );
            }

            if (await _familyRepository.GetCountAsync() <= 0)
            {
                await _familyRepository.InsertAsync(
                    new Family
                    {
                        ProcductName = "测试产品模块",
                        FileName = "测试族文件名称"
                    },
                    autoSave: true
                );
            }

            if (await _familyTreeRepository.GetCountAsync() <= 0)
            {
                //FamilyTree seriesNode = new FamilyTree
                //{
                //    DisplayName = "科睿吊顶·琉晶",
                //};
                //await _familyTreeRepository.InsertAsync(seriesNode, autoSave: true);
                //FamilyTree systemNode = new FamilyTree
                //{
                //    DisplayName = "吊顶",
                //    Children = new List<FamilyTree> { seriesNode },
                //};
                //await _familyTreeRepository.InsertAsync(systemNode, autoSave: true);
                //FamilyTree childrenNode = new FamilyTree
                //{
                //    DisplayName = "模块族库",
                //    Children = new List<FamilyTree> { systemNode },
                //};
                //FamilyTree childrenNode1 = new FamilyTree
                //{
                //    DisplayName = "产品族库",
                //};
                //await _familyTreeRepository.InsertManyAsync(new[] { childrenNode1, childrenNode }, autoSave: true);
                FamilyTree parentNode1 = new FamilyTree
                {
                    DisplayName = "产品固化库"
                };
                FamilyTree parentNode2 = new FamilyTree
                {
                    DisplayName = "项目族库"
                };
                await _familyTreeRepository.InsertManyAsync(new[] { parentNode1, parentNode2 }, autoSave: true);
                //await _familyTreeRepository.InsertAsync(parentNode, autoSave: true);
            }

            if (await _familyLibRepository.GetCountAsync() <= 0)
            {
                string baseurl = @"https://gyhyjysvn.chinayasha.com/svn/Public/appupdate/BDSautocad/FamilyLibCache";
                FamilyLib childrenNode1 = new FamilyLib
                {
                    DisplayName = "xxxxx物料",
                };
                FamilyLib childrenNode2 = new FamilyLib
                {
                    DisplayName = "xxxxx模块",
                };
                await _familyLibRepository.InsertAsync(childrenNode1, autoSave: true);
                await _familyLibRepository.InsertAsync(childrenNode2, autoSave: true);
                await _familyLibRepository.InsertAsync(
                    new FamilyLib
                    {
                        CategoryId = new Guid("3a099423-a529-6f2e-8770-4ed9baa2d190"),
                        DisplayName = "899琉晶镁质覆膜平顶-DD21",
                        Children = new List<FamilyLib> { childrenNode1, childrenNode2 },
                    },
                    autoSave: true
                );
                await _familyLibRepository.InsertAsync(
                    new FamilyLib
                    {
                        CategoryId = new Guid("3a099423-a529-6f2e-8770-4ed9baa2d190"),
                        DisplayName = "599琉晶镁质覆膜贴面平顶-DD21",
                        FilePath = baseurl + "/全户型/10墙顶收口线条-QM00.rfa",
                    },
                    autoSave: true
                );
            }

            if (await _materialSpecificationRepository.GetCountAsync() <= 0)
            {
                MaterialSpecification test1 = new MaterialSpecification
                {
                    MaterialNumber = "0001",
                    ProjectName = "测试项目名称20230322-1",
                    ProjectCode = "1001",
                    FileName = "测试物料清单名称20230322-1",
                    Version = "Beta1.0",
                };
                MaterialSpecification test2 = new MaterialSpecification
                {
                    MaterialNumber = "0002",
                    ProjectName = "测试项目名称20230322-2",
                    ProjectCode = "1002",
                    FileName = "测试物料清单名称20230322-2",
                    Version = "Beta1.0",
                };

                await _materialSpecificationRepository.InsertManyAsync(
                   new List<MaterialSpecification> { test1, test2 },
                    autoSave: true
                );
            }

            if (await _materialSpecificationDetailRepository.GetCountAsync() <= 0)
            {
                MaterialSpecificationDetail test1 = new MaterialSpecificationDetail
                {
                    Name = "工业化项目-物料规格清单测试",
                    ProjectName = "测试项目名称20230322-1",
                    ProductPlatform = "科睿",
                    ProductSystem = "墙面",
                    ComponentPosition = "墙面",
                    MaterialName = "科耐8-平面板",
                    DrawingWidth = 226,
                    DrawingThickness = 8,
                    MaterialCount = 1,
                    MaterialUnit = "张",
                };

                MaterialSpecificationDetail test2 = new MaterialSpecificationDetail
                {
                    Name = "工业化项目-物料规格清单测试-2",
                    ProjectName = "测试项目名称20230410-1",
                    ProductPlatform = "科睿",
                    ProductSystem = "地面",
                    ComponentPosition = "地面",
                    MaterialName = "科耐6-平面板",
                    DrawingWidth = 116,
                    DrawingThickness = 4,
                    MaterialCount = 1,
                    MaterialUnit = "张",
                };
                await _materialSpecificationDetailRepository.InsertManyAsync(
                   new List<MaterialSpecificationDetail> { test1, test2 },
                    autoSave: true
                );
            }

            if (await _requisitionRepository.GetCountAsync() <= 0)
            {
                await _requisitionRepository.InsertAsync(
                    new RequisitionList
                    {
                        Name = "1984",
                        FilePath = ""
                    },
                    autoSave: true
                );
            }
        }
    }
}
