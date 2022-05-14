using project.App.Models;
using project.lib;
using System;
using System.Linq;

namespace project.App
{
    internal class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            const string fileCars = "cars.dat";
            const string fileModels = "models.dat";
            const string fileBrands = "brands.dat";

            Helpers.Init("Project like Turbo.az v1.0");

            // load cars data
            var carsGraph = Helpers.LoadFromFile(fileCars);
            Car[] cars = default;
            if (carsGraph == null)
            {
                cars = new Car[0];
            }
            else
            {
                cars = (Car[])carsGraph;
                int max = 0;
                foreach (Car car in cars)// save edende max id'ni tapib counter'e set edirik.
                {
                    if (car.Id > max)
                    {
                        max = car.Id;
                    }
                }
                Car.SetCounter(max);
            }

            // load model data
            var modelsGraph = Helpers.LoadFromFile(fileModels);
            Model[] models = null;
            if (modelsGraph == null)
            {
                models = new Model[0];
            }
            else
            {
                models = (Model[])modelsGraph;
                int max = 0;
                foreach (Model model in models)
                {
                    if (model.Id > max)
                    {
                        max = model.Id;
                    }
                }
                Model.SetCounter(max);
            }
            // load brand data
            var brandsGraph = Helpers.LoadFromFile(fileBrands);
            Brand[] brands = null;
            if (brandsGraph == null)
            {
                brands = new Brand[0];
            }
            else
            {
                brands = (Brand[])brandsGraph;
                int max = 0;
                foreach (Brand brand in brands)
                {
                    if (brand.Id > max)
                    {
                        max = brand.Id;
                    }
                }
                Brand.SetCounter(max);
            }

            int id;

        l1:
            Helpers.PrintMenu<MenuStates>();
            MenuStates m = Helpers.ReadMenu("Choose the one: ");

            switch (m)
            {
                //show all cars
                case MenuStates.CarsAll:
                    Console.Clear();
                    Console.WriteLine("List of cars...");
                    foreach (var car in cars)
                    {
                        var model = models.FirstOrDefault(m => m.Id == car.ModelId);
                        Console.WriteLine(car.ToString(model));
                    }
                    goto l1;

                //show car by id
                case MenuStates.CarById:
                    id = Helpers.ReadInt("Car ID : ", 0);

                    if (id == 0)
                    {
                        goto case MenuStates.CarsAll;
                    }

                    var search = cars.FirstOrDefault(c => c.Id == id);

                    if (search != null)
                    {
                        Console.Clear();
                        Helpers.PrintWarning($"Found: {search}");
                        goto l1;
                    }
                    Helpers.PrintError("Car not found !");
                    goto case MenuStates.CarById;

                //Car add
                case MenuStates.CarAdd:

                    Helpers.ShowAll<Model>(models);
                    int modelId;
                l2:
                    modelId = Helpers.ReadInt("Enter the model id : ", minValue: 1);
                    var selectedModel = new Model(modelId);

                    if (Array.IndexOf(models, selectedModel) == -1)
                    {
                        Helpers.PrintError("Select from the list !");
                        goto l2;
                    }

                    var carsLen = cars.Length;
                    Array.Resize(ref cars, carsLen + 1);

                    cars[carsLen] = new Car();
                    cars[carsLen].ModelId = modelId;
                    cars[carsLen].Year = Helpers.ReadInt("Year of the car : ", 1974);
                    cars[carsLen].Price = Helpers.ReadDouble("Price of the car: ", 0.50);
                    Helpers.PrintMenu<OilType>();
                    OilType ot = Helpers.SelectOil("Type of oil: ");
                    switch (ot)
                    {
                        case OilType.benzin:
                            cars[carsLen].OilType = OilType.benzin.ToString();
                            break;
                        case OilType.dizel:
                            cars[carsLen].OilType = OilType.dizel.ToString();
                            break;
                        default:
                            break;
                    }
                    Helpers.PrintMenu<BanTypes>();
                    BanTypes bt = Helpers.SelectBan("Type of ban: ");
                    switch (bt)
                    {
                        case BanTypes.Sedan:
                            cars[carsLen].BanNovu = BanTypes.Sedan.ToString();
                            break;
                        case BanTypes.Offroader_Suv:
                            cars[carsLen].BanNovu=BanTypes.Offroader_Suv.ToString();
                            break;
                        case BanTypes.Rodster:
                            cars[carsLen].BanNovu = BanTypes.Rodster.ToString();
                            break;
                        default:
                            break;
                    }

                    Console.Clear();
                    goto case MenuStates.CarsAll;


                // car edit
                case MenuStates.CarEdit:
                    id = Helpers.ReadInt("Car id : ", 0);

                    if (id == 0)
                    {
                        goto case MenuStates.CarsAll;
                    }

                    var searchByEdit = cars.FirstOrDefault(c => c.Id == id);

                    if (searchByEdit != null)
                    {
                        Console.Clear();
                        Helpers.PrintWarning($"Found: {searchByEdit}");

                        Helpers.ShowAll<Model>(models);
                        int modelIdForEdit;
                    l3:
                        modelIdForEdit = Helpers.ReadInt("Choose the one : ", 1);

                        var selectedModelForEdit = new Model(modelIdForEdit);

                        if (Array.IndexOf(models, selectedModelForEdit) == -1)
                        {
                            Helpers.PrintError("Choose from the list ! ");
                            goto l3;
                        }
                        searchByEdit.ModelId = modelIdForEdit;

                        searchByEdit.Year = Helpers.ReadInt("Year of the car: ", 1974);
                        searchByEdit.Price = Helpers.ReadDouble("Price of the car: ");
                        Helpers.PrintMenu<OilType>();
                        OilType editOt = Helpers.SelectOil("Type of oil: ");
                        switch (editOt)
                        {
                            case OilType.benzin:
                                searchByEdit.OilType = OilType.benzin.ToString();
                                break;
                            case OilType.dizel:
                                searchByEdit.OilType = OilType.dizel.ToString();
                                break;
                            default:
                                break;
                        }
                        Helpers.PrintMenu<BanTypes>();
                        BanTypes editBt = Helpers.SelectBan("Type of ban: ");
                        switch (editBt)
                        {
                            case BanTypes.Sedan:
                                searchByEdit.BanNovu = BanTypes.Sedan.ToString();
                                break;
                            case BanTypes.Offroader_Suv:
                                searchByEdit.BanNovu = BanTypes.Offroader_Suv.ToString();
                                break;
                            case BanTypes.Rodster:
                                searchByEdit.BanNovu = BanTypes.Rodster.ToString();
                                break;
                            default:
                                break;
                        }
                        goto case MenuStates.CarsAll;
                    }
                    Helpers.PrintError("Car not found !");
                    goto case MenuStates.CarEdit;

                // remove the car
                case MenuStates.CarRemove:

                    id = Helpers.ReadInt("Car id : ", 0);

                    if (id == 0)
                    {
                        goto case MenuStates.CarsAll;
                    }
                    var searchByRemove = new Car(id);
                    int indexByRemove = Array.IndexOf(cars, searchByRemove);

                    if (indexByRemove == -1)
                    {
                        Helpers.PrintError("Car not found !");
                        goto case MenuStates.CarRemove;
                    }
                    for (int i = indexByRemove; i < cars.Length-1; i++)
                    {
                        cars[i] = cars[i + 1];
                    }
                    Array.Resize(ref cars, cars.Length - 1);
                    goto case MenuStates.CarsAll;

                    // show the Models

                case MenuStates.ModelsAll:

                    //Console.Clear();
                    //Helpers.ShowAll<Model>(models);
                    //goto l1;
                    Console.Clear();
                    Console.WriteLine("List of cars...");
                    foreach (var model in models)
                    {
                        var brand = brands.FirstOrDefault(b => b.Id == model.BrandId);
                        Console.WriteLine(model.ToString(brand));
                    }
                    goto l1;

                //Models with id
                case MenuStates.ModelById:
                    id = Helpers.ReadInt("Id of the model: ", 0);
                    if (id==0)
                    {
                        goto case MenuStates.ModelsAll;
                    }

                    var searchModel = models.FirstOrDefault(m => m.Id == id);

                    if (searchModel!=null)
                    {
                        Console.Clear();
                        Helpers.PrintWarning($"Found: {searchModel}");
                        goto l1;
                    }
                    Helpers.PrintError($"Model not found !");
                    goto case MenuStates.ModelById;

                    // add model
                case MenuStates.ModelAdd:
                    Console.Clear();
                    Helpers.PrintMenu<MenuStates>();
                    Helpers.ShowAll<Brand>(brands);
                    int brandId;
                l4:
                    brandId = Helpers.ReadInt("Enter the brand id: ", minValue: 1);
                    var selectedBrand = new Brand(brandId);

                    if (Array.IndexOf(brands,selectedBrand)==-1)
                    {
                        Helpers.PrintError("Select from the list ! ");
                        goto l4;
                    }

                    int modelLen = models.Length;
                    Array.Resize(ref models, modelLen + 1);

                    models[modelLen] = new Model();
                    models[modelLen].BrandId=brandId;
                    models[modelLen].ModelName = Helpers.ReadString("Model name: ", true);
                    Console.Clear();
                    goto case MenuStates.ModelsAll;

                    //model edit
                case MenuStates.ModelEdit:
                    Helpers.PrintMenu<MenuStates>();
                    id = Helpers.ReadInt("Model id: ",0);

                    if (id==0)
                    {
                        goto case MenuStates.ModelsAll;
                    }

                    var itemByEdit = models.FirstOrDefault(m =>m.Id==id);

                    if (itemByEdit!=null)
                    {
                        Console.Clear();
                        Helpers.PrintWarning($"Found: {itemByEdit}");

                        string nameByEdit = Helpers.ReadString("Model Name: ");

                        if (!string.IsNullOrWhiteSpace(nameByEdit))
                        {
                            itemByEdit.ModelName = nameByEdit;
                        }

                        Helpers.ShowAll<Brand>(brands);
                        int brandIdForEdit;
                    l5:
                        brandIdForEdit = Helpers.ReadInt("Edit Model's brand id: ", minValue: 1);
                        var selectedBrandForEdit = new Brand(brandIdForEdit);

                        if (Array.IndexOf(brands,selectedBrandForEdit)==-1)
                        {
                            Helpers.PrintError("Select from the list!");
                            goto l5;
                        }
                        itemByEdit.BrandId = brandIdForEdit;

                        goto case MenuStates.ModelsAll;
                    }
                    Helpers.PrintError("Model not found !");
                    goto case MenuStates.ModelEdit;

                    // model remove

                case MenuStates.ModelRemove:

                    id = Helpers.ReadInt("Model id: ", 0);

                    if (id==0)
                    {
                        goto case MenuStates.ModelsAll;
                    }
                    var searchModelByRemove = new Model(id);
                    int indexModelByRemove = Array.IndexOf(models,searchModelByRemove);

                    if (indexModelByRemove==-1)
                    {
                        Helpers.PrintError("Model not found! ");
                        goto case MenuStates.ModelRemove;
                    }

                    for (int i = indexModelByRemove; i < models.Length-1; i++)
                    {
                        models[i] = models[i + 1];
                    }

                    Array.Resize(ref models,models.Length-1);
                    goto case MenuStates.ModelsAll;

                    // show all brand
                case MenuStates.BrandsAll:

                    Console.Clear();
                    Helpers.ShowAll<Brand>(brands);
                    goto l1;

                    // brand with id
                case MenuStates.BrandById:

                    id = Helpers.ReadInt("Brand id: ",0);

                    if (id==0)
                    {
                        goto case MenuStates.BrandsAll;
                    }

                    var itemBrand = brands.FirstOrDefault(b => b.Id == id);

                    if (itemBrand!=null)
                    {
                        Console.Clear();
                        Helpers.PrintWarning($"Found: {itemBrand}");
                        goto l1;
                    }
                    Helpers.PrintError("Brand not found !");
                    goto case MenuStates.BrandById;

                    // add brand
                case MenuStates.BrandAdd:
                    int brandsLen = brands.Length;
                    Array.Resize(ref brands, brandsLen + 1);
                    brands[brandsLen] = new Brand();
                    brands[brandsLen].BrandName = Helpers.ReadString("Name of the brand: ");
                    Console.Clear();
                    goto case MenuStates.BrandsAll;

                    // edit brand
                case MenuStates.BrandEdit:

                    id = Helpers.ReadInt("Brand id: ",0);

                    if (id==0)
                    {
                        goto case MenuStates.BrandsAll;
                    }

                    var itemBrandByEdit=brands.FirstOrDefault(b=>b.Id==id);

                    if (itemBrandByEdit!=null)
                    {
                        Console.Clear();
                        Helpers.PrintWarning($"Found: {itemBrandByEdit}");

                        string nameBrandByEdit = Helpers.ReadString("Name of the brand: ");

                        if (!string.IsNullOrWhiteSpace(nameBrandByEdit))
                        {
                            itemBrandByEdit.BrandName = nameBrandByEdit;
                        }

                        goto case MenuStates.BrandsAll;
                    }
                    Helpers.PrintError("Brand not found ! ");
                    goto case MenuStates.BrandEdit;

                    // remove the brand
                case MenuStates.BrandRemove:

                    id = Helpers.ReadInt("Brand id: ", 0);

                    if (id==0)
                    {
                        goto case MenuStates.BrandsAll;
                    }

                    var searchBrandByRemove = new Brand(id);
                    int indexBrandByRemove = Array.IndexOf(brands, searchBrandByRemove);

                    if (indexBrandByRemove==-1)
                    {
                        Helpers.PrintError("Brand not found !");
                        goto case MenuStates.BrandRemove;
                    }

                    for (int i = indexBrandByRemove; i < brands.Length-1; i++)
                    {
                        brands[i] = brands[i + 1];
                    }
                    Array.Resize(ref brands, brands.Length - 1);
                    goto case MenuStates.BrandsAll;

                    // save to file
                case MenuStates.Save:

                    Helpers.SaveToFile(fileCars, cars);
                    Helpers.SaveToFile(fileModels, models);
                    Helpers.SaveToFile(fileBrands, brands);

                    Console.Clear();
                    Console.WriteLine("Saved!");
                    goto l1;


                case MenuStates.Exit:

                    Helpers.PrintError("Tesdiq ucun <Enter> duymesini sixin.Eks halda Menuya qayidacaq.");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    goto l1;
                default:
                    break;
            }

        }
    }
}
