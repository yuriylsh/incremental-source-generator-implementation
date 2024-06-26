using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoProperty.Generator;

[Generator]
public class AutoPropertyGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {   
        // Create a simple filter to find classes that might implement interfaces
        // the <string> generic parameter is the type that is returned by the transform function
        IncrementalValuesProvider<string> pipeline = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: static (node, _) => NodeIsEligibleForGeneration(node),
            transform: static (ctx, _) => TransformNode(ctx));

        context.RegisterSourceOutput(pipeline, GenerateOutput);
    }


    private static bool NodeIsEligibleForGeneration(SyntaxNode node)
        => node is TypeDeclarationSyntax {BaseList.Types.Count: > 0} and (ClassDeclarationSyntax or RecordDeclarationSyntax);

    private static string TransformNode(GeneratorSyntaxContext generatorContext)
        => ((TypeDeclarationSyntax) generatorContext.Node).Identifier.ValueText;

    private static void GenerateOutput(SourceProductionContext context, string classIdentifier /* this is what TransformNode returns */)
    {
        context.AddSource($"{classIdentifier}.g.cs", $"/// Generated code for {classIdentifier}");
    }
}