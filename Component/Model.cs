

/*
 *Конфигурация подключения https://metanit.com/sharp/efcore/1.5.php
 *1.Переопределение у класса контекста данных метода OnConfiguring()
 *2.Передача конфигурации в конструктор базового класса DbContext
 *  2.1 Строка подключения в коде
 *  2.2 Строка подключения в файле (*.json): в "Program" и контексте
 */
/*  Логирование операций https://metanit.com/sharp/efcore/1.6.php
*  1. Вывод в консоль
*  2. Вывод в окно "Вывод (Оutput)" при помощи метода Debug.WriteLine().
*  3. Вывод в файл после закрытия программы (не сегодня)
*  4. Настройки логгирования (практика -- вечером)
*  4.1. Уровни логгирования
*  4.2. Конкретизация сообщений
*  4.3. Категории сообщений 
*/
/*
 * Дополнительная конфигурация  при помощи аннотаций и `Fluent API` https://metanit.com/sharp/efcore/2.3.php
 * 1. Включение `Fluent API`
 * 2. Аннотации
 * 3. Условности
 */
/* Определение моделей https://metanit.com/sharp/efcore/2.3.php
 * 1. Включений сущности в модель 
 * 2. Ссылочные типы nullable и DBSet 
 * 3. Включении сущности в модель без DbSet
 * 4. Исключение из модели
 */
/* Приведение базы данных в нужный вид
 * 1. Сопоставление полей и столбцов (частично уже есть)
 * https://metanit.com/sharp/efcore/2.6.php
 * 1.1 Соглашения (есть)
 * 1.2 Аннотации (для таблицы "Рецептуры")
 * 1.3 Fluent API (для таблицы "Категории") *          
 * 2. Значения по умолчанию (таблица "Рецептура", поле "Автор")
 * ("Генерация значений свойст и столбцов" https://metanit.com/sharp/efcore/2.8.php) 
 *          modelBuilder.Entity<User>().Property(u => u.Age).HasDefaultValue(18);
 * 3. Ограничения и проверки
 *      метод HasCheckConstraint()
 *          для 7+ версий: modelBuilder.Entity<User>().ToTable(t => t.HasCheckConstraint("ValidAge", "Age > 0 AND Age < 120"));
 *          для более ранних (с 3?) версий: modelBuilder.Entity<Amount>(t => t.HasCheckConstraint("amountsMax", "[Amounts] < 5000"));
 * 3.1 Длины, аннотация 
 * 3.2 Длина, Fluent API
 *       modelBuilder.Entity<User>().Property(b => b.Name).HasMaxLength(50);
 * https://metanit.com/sharp/efcore/2.9.php для Sqlite ограничкение длины не работает
 * 4. Аннотация внешних ключей
 * 5. Поля, свойства и конструкторы сущностей (уже есть)
 * 6. Обязательные и необязательные поля (уже есть, записать в тетрадь результаты опытов)
 * 7. Иннициализация базы данных начальными данными (уже есть рабочий минимум)
 */
/*
 * Генерация значений свойств и столбцов https://metanit.com/sharp/efcore/2.8.php
 */

/*
using System;
using System.IO; // file system
using System.Collections.Generic; //List<T> and c.t.r.
using System.Linq;
using System.ComponentModel.DataAnnotations; // [Annotation]
using System.ComponentModel.DataAnnotations.Schema; // [Tables, Columns]
using Microsoft.Extensions.Configuration; // ConfigurationBuilder
using Microsoft.Extensions.Logging; // loggings` levels
using Microsoft.EntityFrameworkCore; // data base
*/

using System;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // [Annotation]
using System.ComponentModel.DataAnnotations.Schema;
using MajPAbGr_project;
using System.Drawing;
using System.Runtime.CompilerServices; // [Tables, Columns]

namespace FormEF_test //EFSqlite_test
{
    public class Technology
    {
        public Technology() { }

        public Technology(int id, string name, string note)
        {
            Id = id;
            Name = name;
            Note = note;
        }

        public int Id { get; set; }      
        
        public string Name { get; set; }
        
        public string Note { get; set; }


        public void createTableIfNotExist(tbController db)
        {
            string query = db.TechnologiesQuery;
            db.Edit(query);
        }

        public int WriteTechnologyUsingQuery(tbController db)
        { 
            string query = $"insert into Technology (name, technology) values ({Name}, {Note}; last_insert_rowid()";            
            Id = int.Parse(db.Count(query));
            return Id;
        }

        public int RemoveTechnologyUsingQuery(tbController db)
        {
            string query = $"delete from Technology where id = {Id};";           
            return db.Edit(query);
        }

        public int UpdateDataUsingQuery(tbController db)
        {
            string query = $"update Technology set name = '{Name}', technology = '{Note}' where id = {Id};";
            return db.Edit(query);
        }
    }
    
    
    public class Card
    {
        
        public int Id { get; set; }
        public string Name { get; set; }       
        public string Description { get; set; }       
    }

    public class Chains
    {
        public string Id { get; set; }
        public int TechnologyId { get; set; }       
        public int CardId { get; set; }
        public int Note { get; set; }


        public Technology Technology { get; set; }
        public Card Card { get; set; }

    }
    
    //classes for receptures
     public class Ingredient
    {
        int id;        
        string name;

        public Ingredient() { }

        public Ingredient(string name)
        {
            this.name = name;
        }

        public Ingredient (Item item)
        {
            this.id = item.id;
            this.name = item.name;
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Item IngredientAsItem()
        {
            Item item = new Item();
            item.createItem(Id, Name);
            return item;
        }
    }

    public class Category
    {
        int id;
        string name;

        public Category() { }

        public Category(string name)
        {
            this.name = name;
        }

        public Category(Item item)
        {
            this.id = item.id;
            this.name = item.name;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Item CategoryAsItem()
        {
           Item item = new Item();
           item.createItem(Id, Name);
           return item;
        }
    }

    public class Recepture
    {
        private int id, id_category;
        int? id_main, id_technology;
        string name, source, author, path, description;
        private int category;

        public Recepture() { }

        public Recepture(string name, int category)
        {
            this.name = name;
            this.category = category;
        }

        public Recepture(string name, int id_category, int id_technology)
        {
            this.name = name;
            this.id_category = id_category;
            this.id_technology = id_technology;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int? IngredientId
        {
            get { return id_main; }
            set { id_main = value; }
        }
   
        public int CategoriesId
        {
            get { return id_category; }
            set { id_category = value; }
        }

        public int? TechnologyId
        {
            get { return id_technology; }
            set { id_technology = value; }
        }

        public Ingredient Ingredient { get; set; }
        public Category Categories { get; set; }
        public Technology Technology { get; set; }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

              
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        /* внешние ключи и навигационные свойства*/
        /*https://metanit.com/sharp/efcore/3.1.php */


        public void setFirtsIngredientAsMain()
        {
            //
        }

        public override string ToString()
        {
            string t = "Recepture info";
            return t;
        }
    }

    public class Amount
    {
        private int id, id_ingredient, id_recepture, id_mes;
        private double amount = 0;

        public Amount() { }

        public Amount(int id_ingredient, int id_mesaure, int id_recepture, double amount) // full
        {
            this.id_ingredient = id_ingredient;
            this.id_mes = id_mesaure;
            this.id_recepture = id_recepture;
            this.amount = amount;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        

        public int ReceptureId
        {
            get { return id_recepture; }
            set { id_recepture = value; }
        }
        
        public int IngredientId
        {
            get { return id_ingredient; }
            set { id_ingredient = value; }
        }

        public int MesauresRodId
        {
            get { return id_mes; }
            set { id_mes = value; }
        }
        
        public double Amounts
        {
            get { return amount; }
            set { amount = value; }
        }

        public Recepture Recepture { get; set; }
        public Ingredient Ingredient { get; set; }
        public UnitOfIngredientMesaure MesauresRod { get; set; }

        public override string ToString()
        {
            return Ingredient.Name + " " + amount.ToString();
        }
    }

    public class Recipe
    {
        int id, id_recepture;
        string name;
        double coeff;

        public Recipe() { }
        public Recipe(int id_recepture, string name, double coeff)
        {
            this.id_recepture = id_recepture;
            this.name = name;
            this.coeff = coeff;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public int ReceptureId
        {
            get { return id_recepture; }
            set { id_recepture = value; }
        }
        
        public double Coeff
        {
            get { return coeff; }
            set { coeff = value; }
        }

        public Recepture Recepture { get; set; }
    }


    /*
     *  Mesaures convertation
     */
    public class UnitOfIngredientMesaure
    {
        int id;
        string name;       

        public UnitOfIngredientMesaure() { }

        public UnitOfIngredientMesaure(string name)
        {
            this.name = name;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
            return name;
        }
    }

}
