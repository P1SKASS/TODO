using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TODO.Models;

public class XmlStorageService
{
    private readonly string _filePath;

    public XmlStorageService(string filePath)
    {
        _filePath = filePath;
    }

    public List<TaskForList> LoadTasks()
    {
        var doc = XDocument.Load(_filePath);
        var categories = LoadCategories();

        return doc.Root.Element("Tasks").Elements("Task")
            .Select(x => new TaskForList
            {
                Id = (int)x.Element("Id"),
                TaskName = (string)x.Element("TaskName"),
                Deadline = (DateTime?)(string.IsNullOrWhiteSpace((string)x.Element("Deadline")) ? null : DateTime.Parse((string)x.Element("Deadline"))),
                CategoryId = (int)x.Element("CategoryId"),
                Completed = (bool)x.Element("Completed"),
                Category = categories.FirstOrDefault(c => c.Id == (int)x.Element("CategoryId"))
            }).ToList();
    }

    public List<Category> LoadCategories()
    {
        var doc = XDocument.Load(_filePath);
        return doc.Root.Element("Categories").Elements("Category")
            .Select(x => new Category
            {
                Id = (int)x.Element("Id"),
                CategoryName = (string)x.Element("CategoryName")
            }).ToList();
    }

    public void SaveTasks(List<TaskForList> tasks)
    {
        var doc = XDocument.Load(_filePath);
        doc.Root.Element("Tasks").ReplaceAll(tasks.Select(task => new XElement("Task",
            new XElement("Id", task.Id),
            new XElement("TaskName", task.TaskName),
            new XElement("Deadline", task.Deadline?.ToString("yyyy-MM-dd")),
            new XElement("CategoryId", task.CategoryId),
            new XElement("Completed", task.Completed))));
        doc.Save(_filePath);
    }
}
