# Generating docs with DocFx

[DocFX Documentation](https://dotnet.github.io/docfx/)

Open terminal in Visual Studio which is the Developer Powershell - **View | Terminal**.
Change to the docs folder at the repository root.

```
cd docs
```


Run this command to install **docfx** on the machine if not already installed.
This will install or update docfx.
```
dotnet tool update -g docfx
```


Run this command to generate the Help files:
```
docfx docfx.json --serve
```
This will generate the documentation

You should be able to use the browser to see the docs at http://localhost:8080

The docs themselves are a static website in the docs/_site folder.

To view the docs without regenerating them run
```
docfx serve _site
```

If you have any issues viewing the localhost url try: http://127.0.0.1:8080/


