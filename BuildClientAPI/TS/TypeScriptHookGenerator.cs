﻿using System.Text;

public static class TypeScriptHookGenerator
{
    public static void GenerateFile(List<MethodDetails> methods, string path)
    {

        foreach (MethodDetails method in methods.Where(a => a.IsGet))
        {
            StringBuilder content = new();
            content.Append(AddImports(method));
            content.Append(GenerateInterfaces(method));

            if (method.IsGetPaged)
            {

                content.Append(GeneratePagedHookContent(method));
            }
            else if (method.IsGetCached)
            {
                content.Append(GenerateGetCachedHookContent(method));
            }
            else
            {
                content.Append(GenerateHookContent(method));
            }


            content.Append(GenerateFooterContent(method));

            string fileName = $"use{method.Name}.tsx";
            string filePath = Path.Combine(path, fileName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(filePath, content.ToString());
        }
    }

    private static string GenerateInterfaces(MethodDetails method)
    {
        StringBuilder content = new();
        if (method.IsGetPaged)
        {
            content.AppendLine($"interface ExtendedQueryHookResult extends QueryHookResult<PagedResponse<{method.ReturnEntityType}> | undefined> {{}}");
        }
        else
        {
            string ret = method.ReturnEntityType;
            if (method.IsList && !ret.EndsWith("[]"))
            {
                ret += "[]";
            }

            content.AppendLine($"interface ExtendedQueryHookResult extends QueryHookResult<{ret} | undefined> {{}}");
        }

        content.AppendLine("interface Result extends ExtendedQueryHookResult {");
        content.AppendLine("  Clear: () => void;");
        content.AppendLine("  SetField: (fieldData: FieldData) => void;");
        content.AppendLine("  SetIsForced: (force: boolean) => void;");
        content.AppendLine("  SetIsLoading: (isLoading: boolean, query: string) => void;");
        content.AppendLine("}");

        return content.ToString();
    }

    private static string AddImports(MethodDetails method)
    {
        StringBuilder content = new();
        string fetchActionName = $"fetch{method.Name}";
        string p = "QueryHookResult";
        if (method.IsGetPaged)
        {
            p += ",GetApiArgument";
        }
        content.AppendLine($"import {{ {p} }} from '@lib/apiDefs';");
        content.AppendLine("import store, { RootState } from '@lib/redux/store';");
        content.AppendLine("import { useAppDispatch, useAppSelector } from '@lib/redux/hooks';");
        content.AppendLine($"import {{ clear, setField, setIsForced, setIsLoading }} from './{method.Name}Slice';");
        content.AppendLine("import { useCallback,useEffect } from 'react';");

        //if (!method.IsGetCached)
        //{
        content.AppendLine($"import {{ {fetchActionName} }} from './{method.Name}Fetch';");
        //}

        string? a = Utils.IsTSGeneric(method.ReturnEntityType);
        List<string> pList = [];
        if (!string.IsNullOrEmpty(a))
        {
            pList.Add(a);
        }

        if (method.IsGetPaged)
        {
            pList.Add("PagedResponse");
        }
        else if (method.IsGet && !string.IsNullOrEmpty(method.TsParameter))
        {
            pList.Add(method.TsParameter);
        }

        content.AppendLine($"import {{FieldData, {string.Join(",", pList)} }} from '@lib/smAPI/smapiTypes';");
        content.AppendLine();
        return content.ToString();
    }

    private static string GenerateHeader(MethodDetails method)
    {
        StringBuilder content = new();
        content.AppendLine($"  const isForced = useAppSelector((state) => state.{method.Name}.isForced ?? false);");
        content.AppendLine();
        content.AppendLine("  const SetIsForced = useCallback(");
        content.AppendLine("    (forceRefresh: boolean): void => {");
        content.AppendLine("      dispatch(setIsForced({ force: forceRefresh }));");
        content.AppendLine("    },");
        content.AppendLine("    [dispatch]");
        content.AppendLine("  );");
        return content.ToString();
    }

    private static string GeneratePagedSelectors(MethodDetails method)
    {
        StringBuilder content = new();
        content.AppendLine($"const selectData = (state: RootState) => {{");
        content.AppendLine($"    const defaultData = {{}} as PagedResponse<{method.ReturnEntityType}>;");
        content.AppendLine($"    if (query === undefined) return defaultData;");
        content.AppendLine($"    return state.{method.Name}.data[query] || defaultData;");
        content.AppendLine($"  }};");
        content.AppendLine($"const data = useAppSelector(selectData);");
        content.AppendLine();

        content.AppendLine($"const selectError = (state: RootState) => {{");
        content.AppendLine($"    if (query === undefined) return undefined;");
        content.AppendLine($"    return state.{method.Name}.error[query] || undefined;");
        content.AppendLine($"  }};");
        content.AppendLine($"const error = useAppSelector(selectError);");
        content.AppendLine();

        content.AppendLine($"const selectIsError = (state: RootState) => {{");
        content.AppendLine($"    if (query === undefined) return false;");
        content.AppendLine($"    return state.{method.Name}.isError[query] || false;");
        content.AppendLine($"  }};");
        content.AppendLine($"const isError = useAppSelector(selectIsError);");
        content.AppendLine();

        content.AppendLine($"const selectIsLoading = (state: RootState) => {{");
        content.AppendLine($"    if (query === undefined) return false;");
        content.AppendLine($"    return state.{method.Name}.isLoading[query] || false;");
        content.AppendLine($"  }};");
        content.AppendLine($"const isLoading = useAppSelector(selectIsLoading);");

        return content.ToString();
    }

    private static string GenerateCachedSelectors(MethodDetails method)
    {
        StringBuilder content = new();
        content.AppendLine($"const selectData = (state: RootState) => {{");
        content.AppendLine($"    const defaultData = [] as {method.TsReturnType};");
        content.AppendLine($"    if (param === undefined) return defaultData;");
        content.AppendLine($"    return state.{method.Name}.data;");
        content.AppendLine($"  }};");
        content.AppendLine($"const data = useAppSelector(selectData);");
        content.AppendLine();

        content.AppendLine($"const selectError = (state: RootState) => {{");
        content.AppendLine($"    if (param === undefined) return undefined;");
        content.AppendLine($"    return state.{method.Name}.error[param] || undefined;");
        content.AppendLine($"  }};");
        content.AppendLine($"const error = useAppSelector(selectError);");
        content.AppendLine();

        content.AppendLine($"const selectIsError = (state: RootState) => {{");
        content.AppendLine($"    if (param === undefined) return false;");
        content.AppendLine($"    return state.{method.Name}.isError[param] || false;");
        content.AppendLine($"  }};");
        content.AppendLine($"const isError = useAppSelector(selectIsError);");
        content.AppendLine();

        content.AppendLine($"const selectIsLoading = (state: RootState) => {{");
        content.AppendLine($"    if (param === undefined) return false;");
        content.AppendLine($"    return state.{method.Name}.isLoading[param] || false;");
        content.AppendLine($"  }};");
        content.AppendLine($"const isLoading = useAppSelector(selectIsLoading);");

        return content.ToString();
    }

    private static string GenerateSelectors(MethodDetails method)
    {
        StringBuilder content = new();
        content.AppendLine($"const selectData = (state: RootState) => {{");
        content.AppendLine($"    return state.{method.Name}.data;");
        content.AppendLine($"  }};");
        content.AppendLine($"const data = useAppSelector(selectData);");
        content.AppendLine();

        content.AppendLine($"const selectError = (state: RootState) => {{");
        content.AppendLine($"    return state.{method.Name}.error;");
        content.AppendLine($"  }};");
        content.AppendLine($"const error = useAppSelector(selectError);");
        content.AppendLine();

        content.AppendLine($"const selectIsError = (state: RootState) => {{");
        content.AppendLine($"    return state.{method.Name}.isError;");
        content.AppendLine($"  }};");
        content.AppendLine($"const isError = useAppSelector(selectIsError);");
        content.AppendLine();

        content.AppendLine($"const selectIsLoading = (state: RootState) => {{");
        content.AppendLine($"    return state.{method.Name}.isLoading;");
        content.AppendLine($"  }};");
        content.AppendLine($"const isLoading = useAppSelector(selectIsLoading);");

        return content.ToString();
    }
    private static string GeneratePagedHeader(MethodDetails method)
    {
        StringBuilder content = new();

        content.AppendLine("  const query = JSON.stringify(params);");
        //content.AppendLine($"  const data = useAppSelector((state) => state.{method.Name}.data[query]);");
        //content.AppendLine($"  const error = useAppSelector((state) => state.{method.Name}.error[query] ?? '');");
        //content.AppendLine($"  const isError = useAppSelector((state) => state.{method.Name}.isError[query] ?? false);");
        content.AppendLine($"  const isForced = useAppSelector((state) => state.{method.Name}.isForced ?? false);");
        //content.AppendLine($"  const isLoading = useAppSelector((state) => state.{method.Name}.isLoading[query] ?? false);");

        content.AppendLine();
        content.AppendLine("  const SetIsForced = useCallback(");
        content.AppendLine("    (forceRefresh: boolean): void => {");
        content.AppendLine("      dispatch(setIsForced({ force: forceRefresh }));");
        content.AppendLine("    },");
        content.AppendLine("    [dispatch]");
        content.AppendLine("  );");

        return content.ToString();

    }

    private static string GenerateGetHeader(MethodDetails method)
    {
        StringBuilder content = new();

        content.AppendLine("  const param = params ? JSON.stringify(params) : undefined;");
        //content.AppendLine($"  const data = useAppSelector((state) => state.{method.Name}.data[param]);");
        //content.AppendLine($"  const error = useAppSelector((state) => state.{method.Name}.error[param] ?? '');");
        //content.AppendLine($"  const isError = useAppSelector((state) => state.{method.Name}.isError[param] ?? false);");
        content.AppendLine($"  const isForced = useAppSelector((state) => state.{method.Name}.isForced ?? false);");
        //content.AppendLine($"  const isLoading = useAppSelector((state) => state.{method.Name}.isLoading[param] ?? false);");

        content.AppendLine();
        content.AppendLine("  const SetIsForced = useCallback(");
        content.AppendLine("    (forceRefresh: boolean): void => {");
        content.AppendLine("      dispatch(setIsForced({ force: forceRefresh }));");
        content.AppendLine("    },");
        content.AppendLine("    [dispatch]");
        content.AppendLine("  );");

        return content.ToString();

    }
    private static string GenerateHookContent(MethodDetails method)
    {
        StringBuilder content = new();
        content.AppendLine($"const use{method.Name} = (): Result => {{");

        content.AppendLine("  const dispatch = useAppDispatch();");

        content.AppendLine(GenerateHeader(method));

        content.AppendLine("const SetIsLoading = useCallback(");
        content.AppendLine("  (isLoading: boolean): void => {");
        content.AppendLine("    dispatch(setIsLoading({ isLoading: isLoading }));");
        content.AppendLine("  },");
        content.AppendLine("  [dispatch]");
        content.AppendLine(");");


        if (method.IsGetCached)
        {
            content.AppendLine(GenerateCachedSelectors(method));
        }
        else
        {
            content.AppendLine(GenerateSelectors(method));
        }



        content.AppendLine();

        content.AppendLine($"  useEffect(() => {{");
        content.AppendLine($"    const state = store.getState().{method.Name};");
        content.AppendLine("    if (data === undefined && state.isLoading !== true && state.isForced !== true) {");
        content.AppendLine("      SetIsForced(true);");
        content.AppendLine("    }");
        content.AppendLine("  }, [SetIsForced, data]);");
        content.AppendLine();
        return content.ToString();
    }

    private static string GenerateGetCachedHookContent(MethodDetails method)
    {
        StringBuilder content = new();

        content.AppendLine($"const use{method.Name} = (params?: {method.TsParameter}): Result => {{");

        content.AppendLine("  const dispatch = useAppDispatch();");

        content.AppendLine(GenerateGetHeader(method));


        content.AppendLine("  const SetIsLoading = useCallback(");
        content.AppendLine("    (isLoading: boolean, param: string): void => {");
        content.AppendLine("      dispatch(setIsLoading({ isLoading: isLoading, param: param }));");
        content.AppendLine("    },");
        content.AppendLine("    [dispatch]");
        content.AppendLine("  );");
        content.AppendLine();

        content.AppendLine(GenerateSelectors(method));

        content.AppendLine();

        content.AppendLine("useEffect(() => {");
        content.AppendLine("  if (param === undefined) return;");
        content.AppendLine($"  const state = store.getState().{method.Name};");
        content.AppendLine("  if (data === undefined && state.isLoading[param] !== true && state.isForced !== true) {");
        content.AppendLine("    SetIsForced(true);");
        content.AppendLine("  }");
        content.AppendLine("}, [data, param, SetIsForced]);");
        content.AppendLine();


        return content.ToString();
    }

    private static string GeneratePagedHookContent(MethodDetails method)
    {
        StringBuilder content = new();

        content.AppendLine($"const use{method.Name} = (params?: GetApiArgument | undefined): Result => {{");

        content.AppendLine("  const dispatch = useAppDispatch();");

        content.AppendLine(GeneratePagedHeader(method));

        content.AppendLine("  const SetIsLoading = useCallback(");
        content.AppendLine("    (isLoading: boolean, query: string): void => {");
        content.AppendLine("      dispatch(setIsLoading({ isLoading: isLoading, query: query }));");
        content.AppendLine("    },");
        content.AppendLine("    [dispatch]");
        content.AppendLine("  );");

        content.AppendLine();

        content.AppendLine(GeneratePagedSelectors(method));

        content.AppendLine();

        content.AppendLine("useEffect(() => {");
        content.AppendLine("  if (query === undefined) return;");
        content.AppendLine($"  const state = store.getState().{method.Name};");
        content.AppendLine("");
        content.AppendLine("  if (data === undefined && state.isLoading[query] !== true && state.isForced !== true) {");
        content.AppendLine("    SetIsForced(true);");
        content.AppendLine("  }");
        content.AppendLine("}, [data, query, SetIsForced]);");
        content.AppendLine();
        return content.ToString();
    }

    private static string GenerateFooterContent(MethodDetails method)
    {
        StringBuilder content = new();
        //content.AppendLine(GenerateSelectors(method));

        //content.AppendLine();

        if (method.IsGetPaged)
        {


            content.AppendLine("useEffect(() => {");
            content.AppendLine($"  const state = store.getState().{method.Name};");

            content.AppendLine("  if (state.isLoading[query]) return;");
            content.AppendLine("  if (query === undefined) return;");

            content.AppendLine("  if (data !== undefined && !isForced) return;");
            content.AppendLine();
            content.AppendLine("  SetIsLoading(true, query);");
            content.AppendLine($"  dispatch(fetch{method.Name}(query));");
            content.AppendLine("}, [data, dispatch, isForced, query, SetIsLoading]);");

        }
        else if (method.IsGetCached)
        {

            content.AppendLine("useEffect(() => {");
            content.AppendLine($"  const state = store.getState().{method.Name};");

            content.AppendLine("  if (params === undefined || param === undefined ) return;");
            content.AppendLine("  if (state.isLoading[param]) return;");

            content.AppendLine("  if (data !== undefined && !isForced) return;");
            content.AppendLine();
            content.AppendLine("  SetIsLoading(true, param);");
            content.AppendLine($"  dispatch(fetch{method.Name}(params));");
            content.AppendLine("}, [SetIsLoading, data, dispatch, isForced, param, params]);");

        }
        else
        {


            content.AppendLine("useEffect(() => {");
            content.AppendLine($"  const state = store.getState().{method.Name};");

            content.AppendLine("  if (state.isLoading) return;");
            content.AppendLine("  if (data !== undefined && !isForced) return;");
            content.AppendLine("");


            content.AppendLine("  SetIsLoading(true);");
            content.AppendLine($"  dispatch(fetch{method.Name}());");
            content.AppendLine("}, [SetIsLoading, data, dispatch, isForced]);");

        }

        content.AppendLine("");
        content.AppendLine("const SetField = (fieldData: FieldData): void => {");
        content.AppendLine("  dispatch(setField({ fieldData: fieldData }));");
        content.AppendLine("};");
        content.AppendLine("");
        content.AppendLine("const Clear = (): void => {");
        content.AppendLine("  dispatch(clear());");
        content.AppendLine("};");
        content.AppendLine("");
        content.AppendLine("return {");
        content.AppendLine("  Clear,");
        content.AppendLine("  data,");
        content.AppendLine("  error,");
        content.AppendLine("  isError,");
        content.AppendLine("  isLoading,");
        content.AppendLine("  SetField,");
        content.AppendLine("  SetIsForced,");
        content.AppendLine("  SetIsLoading");
        content.AppendLine("};");
        content.AppendLine("};");
        content.AppendLine("");
        content.AppendLine($"export default use{method.Name};");

        return content.ToString();

    }

}
