AUTOMAKE_OPTIONS=dist-bzip2 dist-zip
BUILD = $(top_builddir)/build

gpc_ASSEMBLY_NAME = Gpc
gpc_DLL = $(gpc_ASSEMBLY_NAME).dll
gpc_ASSEMBLY = $(BUILD)/$(gpc_DLL)
gpc_FILES =  \
	Gpc/Clipper.cs \
	Gpc/IPoly.cs \
	Gpc/OperationType.cs \
	Gpc/PolyDefault.cs \
	Gpc/PolySimple.cs \
	Gpc/VertexType.cs \
	Gpc/ClipTest.cs \
	Gpc/GeomTest.cs \
	Gpc/PolyDefaultTest.cs

test_ASSEMBLY_NAME = test
test_DLL = $(test_ASSEMBLY_NAME).dll
test_ASSEMBLY = $(BUILD)/$(test_DLL)

# Generic
all: $(gpc_ASSEMBLY) $(test_ASSEMBLY)

clean-local:
	$(RM) -rf $(BUILD)
	$(RM) -f *~ */*~ */*/*~
	$(RM) -rf autom4te.cache config.log

$(gpc_ASSEMBLY): $(gpc_FILES) Makefile
	mkdir -p $(BUILD)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ \
		-keyfile:etc/Gpc.snk \
		-doc:$(BUILD)/Gpc.xml \
		/r:System.Drawing \
		$(realpath $(gpc_FILES))
	chmod a-x $@

$(test_ASSEMBLY): $(test_FILES)
	mkdir -p $(BUILD)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ \
		-keyfile:etc/Gpc.snk \
		/d:DEBUG /d:TRACE \
		/r:System.Drawing \
		@nunit_LIBS@ \
		$(realpath $(gpc_FILES))
	chmod a-x $@

#	Delete the file if the nunit fails
	@NUNIT_CONSOLE@ /nologo $@ || rm $(test_ASSEMBLY)

# pkgconfig
pkgconfig_in_files = etc/gpc-sharp-0.0.pc.in
pkgconfigdir= $(prefix)/lib/pkgconfig
pkgconfig_DATA = $(pkgconfig_in_files:.pc.in=.pc)

# Installation
install-data-local:
	@GACUTIL@ /i $(gpc_ASSEMBLY) /f @GACUTIL_FLAGS@

	mkdir -p @libdir@/cli/gpc-0.0
	install $(gpc_ASSEMBLY) @libdir@/cli/gpc-0.0/

# Uninstallation
uninstall-local:
	@GACUTIL@ /u $(gpc_ASSEMBLY_NAME) @GACUTIL_FLAGS@

	-rm @libdir@/cli/gpc-0.0/$(gpc_DLL)
	-rmdir @libdir@/cli/gpc-0.0

# Definition of the various files that need to be cleaned up properly.
CLEANFILES = $(BUILD)/Gpc.xml TestResult.xml

EXTRA_DIST = $(pkgconfig_in_files) etc/Gpc.snk
EXTRA_DIST += $(gpc_FILES)
EXTRA_DIST += $(test_FILES)

DISTCLEANFILES = etc/gpc-sharp-0.0.pc
DISTCLEANFILES += Gpc/AssemblyInfo.cs
