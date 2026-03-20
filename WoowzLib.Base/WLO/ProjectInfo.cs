using System.Reflection;

namespace WLO;

public readonly struct ProjectInfo{
    /// <summary>
    /// Информация об проекте
    /// </summary>
    /// <param name="Name">Название</param>
    /// <param name="Version">Версия</param>
    /// <param name="Author">Автор</param>
    /// <param name="License">Лицензия</param>
    public ProjectInfo(string? Name = null, Version? Version = null, string? Author = null, string? License = null){
        this.Name = Name ?? "Unknown Project";
        this.Version = Version ?? new Version();
        this.Author = Author ?? "Anonymous";
        this.License = License ?? "MIT";
    }
    
    /// <summary>
    /// Название проекта
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// Версия проекта
    /// </summary>
    public readonly Version Version;

    /// <summary>
    /// Автор проекта
    /// </summary>
    public readonly string Author;

    /// <summary>
    /// Лицензия проекта
    /// </summary>
    public readonly string License;
    
    // ----------------------------------------------------------------------

    public override string ToString(){
        return "ProjectInfo[Name: \"" + Name + "\", Version: " + Version + ", Author: \"" + Author + "\", License: \"" + License + "\"]";
    }
}