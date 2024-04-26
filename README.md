# name-sorter

Using Solid principles, this is how I structured the app

### Single Responsibility Principle (SRP):
- NameSorter will handle only sorting of names.
- NameRepository will handle only reading and writing names to/from a file.
### Open/Closed Principle (OCP):
- Extending the sorting functionality without modifying the NameSorter class.
### Liskov Substitution Principle (LSP):
- By not using inheritence, I am following the rule.
### Interface Segregation Principle (ISP):
- The FileBasedNameRepository class implements only the methods it needs.
### Dependency Inversion Principle (DIP):
- The NameSorter class depends on the INameRepository interface, not on any concrete implementation.

# To run the app
1. Open command line and navigate to where app is located (Main folder, not tests)
2. Run "dotnet run unsorted-names-list.txt"
3. cmd will display sorted list
4. to run tests, navigate cmd to "name-sorter-tests" folder and run "dotnet test"
