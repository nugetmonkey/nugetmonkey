# Calling Convention
#   RemoveReference.ps1 "MyCsProj.csproj" 
#   "..\SomeDirectory\SomeProjectReferenceToRemove.dll"
param($path, $ReferenceHint)

$XPath = [string]::Format("//a:ItemGroup/a:Reference/a:HintPath[starts-with(text(),'{0}')]", $ReferenceHint)   

# [System.Console]::WriteLine("");
# [System.Console]::WriteLine("XPATH IS {0}", $XPath) 
# [System.Console]::WriteLine("");

$proj = [xml](Get-Content $path)
# [System.Console]::WriteLine("Loaded project {0} into {1}", $path, $proj)

[System.Xml.XmlNamespaceManager] $nsmgr = $proj.NameTable
$nsmgr.AddNamespace('a','http://schemas.microsoft.com/developer/msbuild/2003')
$nodes = $proj.SelectNodes($XPath, $nsmgr)

# if (!$node)
# {
#     [System.Console]::WriteLine("");
#     [System.Console]::WriteLine("Cannot find node with XPath {0}", $XPath)
#     [System.Console]::WriteLine("");
#     exit
# }
if ($nodes){
	foreach ($node in $nodes) {
		[System.Console]::WriteLine("Removing node {0}", $node)
		$node.ParentNode.ParentNode.ParentNode.RemoveChild($node.ParentNode.ParentNode);
	}
    

    $proj.Save($path)
}

exit 0