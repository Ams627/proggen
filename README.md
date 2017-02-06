# proggen
Solution generator for Visual Studio
This is a command line solution generator and used as an alternative to the new project wizard. It
sets up a new solution containing one new project. It then opens visual studio and builds the project - that's where its job ends.

A new generator class (i.e. one that implements the IGenerator interface) is added for each type of project. For example,
there is one for a c++ console application project, one for a c# wpf project, one for a .net core project. The
GeneratorManager class uses reflection to register all such generators. Adding another generator is simply
a matter of defining a class that implements the IGenerator interface - that's all that is required.
