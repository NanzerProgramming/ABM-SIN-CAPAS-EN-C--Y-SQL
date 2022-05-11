using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ABM_con_el_PROFE
{
    public partial class Empleados : Form
    {
        public Empleados()
        {
            InitializeComponent();
        }

        #region CONECCION
        SqlConnection conn = new SqlConnection("Password=7d26t13ar4;Persist Security Info=True;User ID=Nanzer_SQLLogin_1;Initial Catalog=ProyectoNanzerClima;Data Source=ProyectoNanzerClima.mssql.somee.com"); //CONEXION PÚBLICA
        #endregion

        #region SELECT
        private void btnRefrescar_Click_1(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand consulta = new SqlCommand("select * from Empleados", conn);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = consulta;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvEmpleados.DataSource = tabla;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                Clear();
            }
        }
        #endregion

        #region UPDATE
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"UPDATE Empleados SET Apellido = @Apellido, Nombre = @Nombre, 
                                FechaNacimiento =@FechaNacimiento, CalleDom = @CalleDom, 
                                NumeroDom = @NumeroDom, PisoDom = @PisoDom, DeptoDom = @DeptoDom, 
                                CiudadDom = @CiudadDom WHERE Legajo = @Legajo";
                conn.Open();
                SqlCommand comando = new SqlCommand(query, conn);

                comando.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                comando.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                comando.Parameters.AddWithValue("@FechaNacimiento", dtpFechaNac.Value);
                comando.Parameters.AddWithValue("@CalleDom", txtCalle.Text);
                comando.Parameters.AddWithValue("@NumeroDom", txtNumero.Text);
                comando.Parameters.AddWithValue("@PisoDom", txtPiso.Text);
                comando.Parameters.AddWithValue("@DeptoDom", txtDepto.Text);
                comando.Parameters.AddWithValue("@CiudadDom", txtCiudad.Text);
                comando.Parameters.AddWithValue("@Legajo", txtLegajo.Text);

                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Actualizado, por favor refresque la tabla.");
            }
        }
        #endregion

        #region DELETE
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = @"DELETE from Empleados WHERE Legajo = @Legajo";
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("@Legajo", txtLegajo.Text);
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                MessageBox.Show("Dato eliminado con exito, por favor refresque la tabla.");
            }
        }
        #endregion

        #region INSERT
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand query = new SqlCommand("INSERT INTO Empleados(Apellido, Nombre, FechaNacimiento, CalleDom, NumeroDom, PisoDom, DeptoDom, CiudadDom) values (@Apellido, @Nombre, @FechaNacimiento, @CalleDom, @NumeroDom, @PisoDom, @DeptoDom, @CiudadDom)", conn);

                query.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                query.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                query.Parameters.AddWithValue("@FechaNacimiento", dtpFechaNac.Value);
                query.Parameters.AddWithValue("@CalleDom", txtCalle.Text);
                query.Parameters.AddWithValue("@NumeroDom", txtNumero.Text);
                query.Parameters.AddWithValue("@PisoDom", txtPiso.Text);
                query.Parameters.AddWithValue("@DeptoDom", txtDepto.Text);
                query.Parameters.AddWithValue("@CiudadDom", txtCiudad.Text);

                query.ExecuteNonQuery();

                //Recarga la lista!

                SqlCommand consulta = new SqlCommand("select * from Empleados", conn);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = consulta;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvEmpleados.DataSource = tabla;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                Clear();
            }

        }


        #endregion

        #region EVENTOS
        private void txtApellido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNombre.Focus();
            }
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpFechaNac.Focus();
            }
        }

        private void dtpFechaNac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCalle.Focus();
            }
        }

        private void txtCalle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNumero.Focus();
            }
        }

        private void txtNumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPiso.Focus();
            }
        }

        private void txtPiso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDepto.Focus();
            }
        }

        private void txtDepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCiudad.Focus();
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Empleados_Shown(object sender, EventArgs e)
        {
            txtApellido.Focus();
        }
        #endregion

        #region FUNCIONES
        private void Clear()
        {
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            dtpFechaNac.Value = DateTime.Now;
            txtCalle.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtPiso.Text = string.Empty;
            txtDepto.Text = string.Empty;
            txtCiudad.Text = string.Empty;
        }

        #endregion

    }
}
