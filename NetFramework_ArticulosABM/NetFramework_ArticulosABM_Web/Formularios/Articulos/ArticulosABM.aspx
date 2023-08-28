<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="ArticulosABM.aspx.cs" Inherits="NetFramework_ArticulosABM_Web.Formularios.Articulos.ArticulosABM" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container m-t-5">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Articulo
                    </div>
                    <div class="panel-body">
                        <asp:HiddenField ID="idArt" runat="server" />
                        <div class="row">
                            <label class="col-lg-3">Detalle</label>
                            <div class="col-lg-7">
                                <input id="detalle" name="detalleN" type="text" class="form-control" onchange="valInput_ById('detalle','valDetalle')" />
                                <p id="valDetalle" class="text-danger hiden">*Campo obligatorio</p>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-lg-3">Stock</label>
                            <div class="col-lg-7">
                                <input id="stock" name="stockN" type="text" class="form-control" onchange="valInput_ById('stock','valStock')" />
                                <p id="valStock" class="text-danger hiden">*Campo obligatorio</p>
                            </div>
                        </div>
                        <div class="row m-t-1 m-b-1">
                            <div class="col-lg-12 text-right">
                                <button id="btnVolver" class="btn btn-default" data-toogle="tooltip" title="Volver">Volver</button>
                                <button id="btnModificar" class="btn btn-primary hide" data-toogle="tooltip" title="Modificar">Modificar</button>
                                <button id="btnAgregar" class="btn btn-primary" data-toogle="tooltip" title="Agregar">Agregar</button>
                                <asp:LinkButton ID="lbtnInsertArticulo" OnClick="lbtnInsertArticulo_Click" runat="server"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            document.getElementById('detalle').focus()

            // Obtener la URL actual
            let url = window.location

            // Obtener los parámetros de la URL
            let params = new URLSearchParams(url.search)

            // Obtener el valor del parámetro 'id'
            let id = params.get('id')

            if (id != null) {
                document.getElementById('<%=idArt.ClientID%>').value = id
                document.getElementById('btnModificar').classList.remove('hide')
                document.getElementById('btnAgregar').classList.add('hide')
                CargarDatosById(id)
            }

        });
    </script>

    <script>
        function CargarDatosById(idArt) {
            fetch('ArticulosABM.aspx/GetArticulo_ById', {
                method: 'POST',
                body: JSON.stringify({ id: idArt }),
                headers: { 'Content-Type': 'application/json' },
            })
                .then(response => {
                    console.log(response);
                    return response.json()
                })
                .then(data => {
                    if (data.d != null) {

                        let rta = JSON.parse(data.d)

                        rta.forEach(e => {
                            document.getElementById('detalle').value = e.descripcion
                            document.getElementById('stock').value = e.stock == null ? '0' : e.stock
                        })
                    }
                })
                .catch(error => {
                    toastr.error("Algo salio mal!.")
                    console.log(JSON.stringify(error))
                });
        }
    </script>

    <script>
        document.getElementById('btnAgregar').addEventListener('click', (event) => {
            event.preventDefault()
            document.getElementById('btnAgregar').setAttribute('disabled', 'disabled')
            document.getElementById('btnVolver').setAttribute('disabled', 'disabled')
            InsertArticulo_ById()
        })

        function InsertArticulo_ById() {

            let valDet = valInput_ById('detalle', 'valDetalle')
            let valStock = valInput_ById('stock', 'valStock')

            if (valDet && valStock) {
                document.getElementById('<%= lbtnInsertArticulo.ClientID %>').click()
            } else {
                document.getElementById('btnAgregar').removeAttribute('disabled')
                document.getElementById('btnVolver').removeAttribute('disabled')
            }
        }
    </script>

    <script>
        document.getElementById('btnModificar').addEventListener('click', (event) => {
            event.preventDefault()
            document.getElementById('btnModificar').setAttribute('disabled', 'disabled')
            document.getElementById('btnVolver').setAttribute('disabled', 'disabled')
            UpdateArticulo_ById()
        })

        function UpdateArticulo_ById() {

            let valDet = valInput_ById('detalle', 'valDetalle')
            let valStock = valInput_ById('stock', 'valStock')

            if (valDet && valStock) {

            let data = {
                id: document.getElementById('<%=idArt.ClientID%>').value,
                detalle: document.getElementById('detalle').value,
                stock: document.getElementById('stock').value,
            }

            fetch('ArticulosABM.aspx/UpdateArticulo_ById', {
                method: 'POST',
                body: JSON.stringify(data),
                headers: { 'Content-Type': 'application/json' },
            })
                .then(response => response.json())
                .then(data => {

                    console.log(data.d);
                    let rta = JSON.parse(data.d)

                    if (data.d == 1) {
                        toastr.success("Articulo modificado correctamente.")
                        setTimeout(function () {
                            window.location.href = "Articulos.aspx";
                        }, 3000);
                    }

                    document.getElementById('btnModificar').removeAttribute('disabled')
                    document.getElementById('btnVolver').removeAttribute('disabled')
                })
                .catch(error => {
                    document.getElementById('btnModificar').removeAttribute('disabled')
                    document.getElementById('btnVolver').removeAttribute('disabled')
                    toastr.error("Algo salio mal!.")
                    console.log(JSON.stringify(error))
                });
            } else {
                document.getElementById('btnModificar').removeAttribute('disabled')
                document.getElementById('btnVolver').removeAttribute('disabled')
            }
        }
    </script>

    <script>
        function valInput_ById(idInput, idInputVal) {
            let input = document.getElementById(idInput).value.trim();
            document.getElementById(idInputVal).className = 'hiden';

            if (input == '') {
                document.getElementById(idInputVal).innerText = '*Campo obligatorio.';
                document.getElementById(idInputVal).className = 'text-danger';
                document.getElementById(idInput).focus()
                return false;
            }
            return true;
        }
    </script>

    <script>
        document.getElementById('btnVolver').addEventListener('click', (event) => {
            event.preventDefault()
            window.location.href = 'Articulos.aspx'
        })
    </script>

</asp:Content>
