# HSDEngine
For Azure FunctionApp

동적으로 변수에 데이터 할당할 때 아래 주의
Property 는 Getter/Setter 함수이고, Field 는 변수임.
// tp.GetField or tp.GetProperty
PropertyInfo propA = tp.GetProperty(secNum, BindingFlags.Instance | BindingFlags.Public);
propA.SetValue(opcA, obj.ToString());

