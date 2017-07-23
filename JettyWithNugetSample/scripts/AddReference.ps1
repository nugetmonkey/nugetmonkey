# Calling convension:
#   AddReference.PS1 "Mycsproj.csproj", 
#                    "MyNewDllToReference.dll", 
#                    "MyNewDllToReference"
param([String]$path, [String]$dllRef, [String]$refName)

$proj = [xml](Get-Content $path)
[System.Console]::WriteLine("")
[System.Console]::WriteLine("AddReference {0} on {1}", $refName, $path)

# Create the following hierarchy
#  <Reference Include='{0}'>
#     <HintPath>{1}</HintPath>
#  </Reference>
# where (0) is $refName and {1} is $dllRef

$xmlns = "http://schemas.microsoft.com/developer/msbuild/2003"
$itemGroup = $proj.CreateElement("ItemGroup", $xmlns);
$proj.Project.AppendChild($itemGroup);

$referenceNode = $proj.CreateElement("Reference", $xmlns);
$referenceNode.SetAttribute("Include", $refName);
$itemGroup.AppendChild($referenceNode)

$hintPath = $proj.CreateElement("HintPath", $xmlns);
$hintPath.InnerXml = $dllRef
$referenceNode.AppendChild($hintPath)

$proj.Save($path)