"""Compila las pruebas de dominio en una carpeta temporal, sin otro proyecto de aplicación."""
from pathlib import Path
import os
import subprocess
import tempfile
import xml.etree.ElementTree as ET

raiz = Path(__file__).resolve().parents[1]
with tempfile.TemporaryDirectory(prefix="gestion-pruebas-") as temporal:
    proyecto = ET.Element("Project", Sdk="Microsoft.NET.Sdk")
    propiedades = ET.SubElement(proyecto, "PropertyGroup")
    for nombre, valor in {"OutputType": "Exe", "TargetFramework": "net8.0", "ImplicitUsings": "enable", "Nullable": "enable"}.items():
        ET.SubElement(propiedades, nombre).text = valor
    fuentes = ET.SubElement(proyecto, "ItemGroup")
    for carpeta in ["model", "repository", "service", "archivos", "util"]:
        for fuente in (raiz / carpeta).glob("*.cs"):
            ET.SubElement(fuentes, "Compile", Include=str(fuente))
    ET.SubElement(fuentes, "Compile", Include=str(raiz / "tests" / "Pruebas.cs"))
    destino = Path(temporal) / "Pruebas.csproj"
    ET.ElementTree(proyecto).write(destino, encoding="utf-8")
    subprocess.run([os.environ.get("DOTNET", "dotnet"), "run", "--project", str(destino), "--", str(raiz)], check=True)
