﻿using System.Text;

public static class StoreGenerator
{
    public static void GenerateFile(List<MethodDetails> methods, string filePath)
    {
        StringBuilder content = new();

        content.Append(AddImports(methods));
        content.Append(GenerateConfigs());

        content.Append(GenerateReducer(methods));

        string directory = Directory.GetParent(filePath).ToString();
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        File.WriteAllText(filePath, content.ToString());
    }

    private static List<string> Persists = new()
    {
        "selectedPostalCode",
        "selectAll",
        "selectedCountry",
        "selectedItems",
        "selectedStreamGroup",
        "showHidden",
        "showSelections",
        "sortInfo",
        "selectedSMStreams",
        "selectedSMChannel",
        "queryFilter",
        "selectedSMStream",
        "messages"
    };

    private static string GenerateConfigs()
    {
        StringBuilder content = new();
        foreach (var persist in Persists.Order())
        {
            content.AppendLine($"const {persist}Config = {{");
            content.AppendLine($"  key: '{persist}',");
            content.AppendLine($"  storage");
            content.AppendLine($"}};");
        }
        return content.ToString();
    }



    private static string GenerateReducer(List<MethodDetails> methods)
    {
        StringBuilder content = new();
        Dictionary<string, string> reducers = new()
        {
            { "queryAdditionalFilters", "  queryAdditionalFilters: queryFilter" },
        };


        foreach (var method in methods)
        {
            reducers[method.Name] = $"  {method.Name}: {method.Name}";
        }

        foreach (var persist in Persists)
        {
            reducers[persist] = $"  {persist}: persistReducer({persist}Config, {persist})";
        }

        content.AppendLine("export const rootReducer = combineReducers({");

        foreach (var key in reducers.Keys.Order())
        {
            content.AppendLine(reducers[key] + ",");
        }

        content.AppendLine("});");
        content.AppendLine("");

        return content.ToString();
    }

    private static string AddImports(List<MethodDetails> methods)
    {
        StringBuilder content = new();
        Dictionary<string, string> imports = [];

        content.AppendLine("import { combineReducers } from 'redux';");

        foreach (MethodDetails method in methods.Where(a => a.IsGet))
        {
            imports[method.Name] = $"import {method.Name} from '@lib/smAPI/{method.NamespaceName}/{method.Name}Slice';";
        }

        foreach (var persist in Persists)
        {
            imports[persist] = $"import {persist} from '@lib/redux/hooks/{persist}';";
        }


        foreach (var key in imports.Keys.Order())
        {
            content.AppendLine(imports[key]);
        }

        content.AppendLine("import { persistReducer } from 'redux-persist';");
        content.AppendLine("import storage from 'redux-persist/lib/storage';");


        content.AppendLine();
        return content.ToString();
    }

}
