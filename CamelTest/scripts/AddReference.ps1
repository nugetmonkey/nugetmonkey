# Calling convension:
#   AddReference.PS1 "Mycsproj.csproj", 
#                    "MyNewDllToReference.dll", 
#                    "MyNewDllToReference"

# param($path, $Reference)
param([String]$path, [String]$dllRef, [String]$refName)
 
$proj = [xml](Get-Content $path)

$nsmgr = New-Object System.Xml.XmlNamespaceManager($proj.NameTable)
 # [System.Xml.XmlNamespaceManager] $nsmgr = $proj.NameTable
  $xmlns = $proj.DocumentElement.NamespaceURI 
  # "http://schemas.microsoft.com/developer/msbuild/2003"
  $nsmgr.AddNamespace('a',$xmlns)

$XPath = [string]::Format("//a:ItemGroup/a:Reference[@Include='{0}']", $refName)  
$node = $proj.SelectSingleNode($XPath,  $nsmgr  )

if (!$node)
{ 
	# Create the following hierarchy
	#  <Reference Include='{0}'>
	#     <HintPath>{1}</HintPath>
	#  </Reference>
	# where (0) is $refName and {1} is $dllRef

	$itemGroup = $proj.CreateElement("ItemGroup", $xmlns);
	$proj.Project.AppendChild($itemGroup);

	$referenceNode = $proj.CreateElement("Reference", $xmlns);
	$referenceNode.SetAttribute("Include", $refName);
	$itemGroup.AppendChild($referenceNode)

	$hintPath = $proj.CreateElement("HintPath", $xmlns);
	$hintPath.InnerXml = $dllRef
	$referenceNode.AppendChild($hintPath)

	$proj.Save($path)
}