using Hypercube.ModLoader;

var raw = File.ReadAllBytes("HelloMod.dll");
var modLoader = new ModLoader();

var mod = modLoader.Load(raw);

Console.WriteLine(mod.Instance.MetaData);

modLoader.Unload(new ModId("HelloMod"));
