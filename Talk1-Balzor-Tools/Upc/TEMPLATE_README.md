Demo EFxceptions POC Template

This template turns the current solution into a dotnet new template that can be installed and used to quickly scaffold POCs.

Install locally:

1. From the root of this repository run:
   dotnet new --install .

Create a new project from the template:

1. dotnet new demoefx -n MyNewPoC

Notes:
- The template will replace occurrences of the default project name `Upc` with the new project name.
- Rename or remove any folders/files you don't want in the generated template.
