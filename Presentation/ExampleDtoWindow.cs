using _420DA3_07451_Projet_Initial.Business.Abstracts;
using _420DA3_07451_Projet_Initial.Business.Services;
using _420DA3_07451_Projet_Initial.DataAccess.DTOs;
using _420DA3_07451_Projet_Initial.Presentation.Abstracts;
using _420DA3_07451_Projet_Initial.Presentation.Enums;
using Microsoft.IdentityModel.Tokens;

namespace _420DA3_07451_Projet_Initial.Presentation;

/// <summary>
/// Exemple de classe de gestion d'entit� sp�cifique.
/// </summary>
/// <remarks>
/// H�rite de <see cref="Form"/> et impl�mente <see cref="IDtoManagementView{DtoType}"/>.
/// </remarks>
public partial class ExampleDtoWindow : Form, IDtoManagementView<ExampleDTO> {
    private readonly AbstractFacade facade;
    private ExampleDTO workingDtoInstance;
    private ViewIntentEnum? workingViewIntent;

    public ExampleDtoWindow(AbstractFacade facade) {
        this.facade = facade;
        // cr�ation d'une instance de DTO vide juste pour garantir qu'il y aura quelque chose
        // dans le champ associ� et �viter de devoir dealer avec un type nullable.
        this.workingDtoInstance = new ExampleDTO("", null);
        this.InitializeComponent();
    }

    public DialogResult OpenForCreation(ExampleDTO blankInstance) {
        // Set de l'instance � travailler sur et de l'intention
        this.workingDtoInstance = blankInstance;
        this.workingViewIntent = ViewIntentEnum.Creation;

        // Changement du texte du bouton d'action.
        this.actionButton.Text = "Create";
        // Activation des contr�les graphiques pour entrer/modifier les donn�es.
        this.EnableEditableFields();
        // chargement des valeurs de l'instance re�ue en param�tre dans les contr�les graphiques.
        this.SetFields(blankInstance);

        return this.ShowDialog();
    }

    public DialogResult OpenForDeletion(ExampleDTO instance) {
        // Set de l'instance � travailler sur et de l'intention
        this.workingDtoInstance = instance;
        this.workingViewIntent = ViewIntentEnum.Deletion;

        // Changement du texte du bouton d'action.
        this.actionButton.Text = "Delete";
        // D�sctivation des contr�les graphiques pour emp�cher la modification des donn�es.
        this.DisableEditableFields();
        // chargement des valeurs de l'instance re�ue en param�tre dans les contr�les graphiques.
        this.SetFields(instance);

        return this.ShowDialog();
    }

    public DialogResult OpenForEdition(ExampleDTO instance) {
        // Set de l'instance � travailler sur et de l'intention
        this.workingDtoInstance = instance;
        this.workingViewIntent = ViewIntentEnum.Edition;

        // Changement du texte du bouton d'action.
        this.actionButton.Text = "Save";
        // Activation des contr�les graphiques pour entrer/modifier les donn�es.
        this.EnableEditableFields();
        // chargement des valeurs de l'instance re�ue en param�tre dans les contr�les graphiques.
        this.SetFields(instance);

        return this.ShowDialog();
    }

    public DialogResult OpenForVisualization(ExampleDTO instance) {
        // Set de l'instance � travailler sur et de l'intention
        this.workingDtoInstance = instance;
        this.workingViewIntent = ViewIntentEnum.Visualization;

        // Changement du texte du bouton d'action.
        this.actionButton.Text = "OK";
        // D�sctivation des contr�les graphiques pour emp�cher la modification des donn�es.
        this.DisableEditableFields();
        // chargement des valeurs de l'instance re�ue en param�tre dans les contr�les graphiques.
        this.SetFields(instance);

        return this.ShowDialog();
    }

    /// <summary>
    /// Remplis les contr�les graphiques de la fen�tre avec les valeurs des propri�t�s
    /// de l'instance de l'entit� g�r�e re�ue en param�tre.
    /// </summary>
    /// <param name="dto"></param>
    private void SetFields(ExampleDTO dto) {

        this.idTextBox.Text = dto.Id.ToString() ?? "";
        this.nameTextBox.Text = dto.Name ?? "";
        this.descriptionTextBox.Text = dto.Description ?? "";

        if (dto.DateCreated is null) {
            this.dateCreatedBox.Format = DateTimePickerFormat.Custom;
            this.dateCreatedBox.CustomFormat = "";
        } else {
            this.dateCreatedBox.Format = DateTimePickerFormat.Long;
            this.dateCreatedBox.Value = (DateTime) dto.DateCreated;
        }

        if (dto.DateUpdated is null) {
            this.dateUpdatedBox.Format = DateTimePickerFormat.Custom;
            this.dateUpdatedBox.CustomFormat = "";
        } else {
            this.dateUpdatedBox.Format = DateTimePickerFormat.Long;
            this.dateUpdatedBox.Value = (DateTime) dto.DateUpdated;
        }

        if (dto.DateDeleted is null) {
            this.dateDeletedBox.Format = DateTimePickerFormat.Custom;
            this.dateDeletedBox.CustomFormat = "";
        } else {
            this.dateDeletedBox.Format = DateTimePickerFormat.Long;
            this.dateDeletedBox.Value = (DateTime) dto.DateDeleted;
        }

    }

    /// <summary>
    /// D�sactive les contr�les graphiques des champs de donn�es
    /// </summary>
    private void DisableEditableFields() {
        this.nameTextBox.Enabled = false;
        this.descriptionTextBox.Enabled = false;
    }

    /// <summary>
    /// Active les contr�les graphiques des champs de donn�es
    /// </summary>
    private void EnableEditableFields() {
        this.nameTextBox.Enabled = true;
        this.descriptionTextBox.Enabled = true;
    }

    /// <summary>
    /// Actions � faire lorsque le bouton d'action est cliqu� pour l'intention de cr�ation
    /// </summary>
    private void DoCreateAction() {
        if (!ExampleDTO.ValidateName(this.nameTextBox.Text)) {
            this.nameTextBox.Invalidate();
            return;
        }
        if (!ExampleDTO.ValidateDescription(this.descriptionTextBox.Text)) {
            this.descriptionTextBox.Invalidate();
            return;
        }
        this.workingDtoInstance.Name = this.nameTextBox.Text;
        this.workingDtoInstance.Description = this.descriptionTextBox.Text.IsNullOrEmpty() ? null : this.descriptionTextBox.Text;

        this.DialogResult = DialogResult.OK;
    }

    /// <summary>
    /// Actions � faire lorsque le bouton d'action est cliqu� pour l'intention de visualisation
    /// </summary>
    private void DoVisualizeAction() {
        this.DialogResult = DialogResult.OK;
    }

    /// <summary>
    /// Actions � faire lorsque le bouton d'action est cliqu� pour l'intention d'�dition
    /// </summary>
    private void DoEditAction() {
        if (!ExampleDTO.ValidateName(this.nameTextBox.Text)) {
            this.nameTextBox.Invalidate();
            return;
        }
        if (!ExampleDTO.ValidateDescription(this.descriptionTextBox.Text)) {
            this.descriptionTextBox.Invalidate();
            return;
        }
        this.workingDtoInstance.Name = this.nameTextBox.Text;
        this.workingDtoInstance.Description = this.descriptionTextBox.Text.IsNullOrEmpty() ? null : this.descriptionTextBox.Text;

        this.DialogResult = DialogResult.OK;
    }

    /// <summary>
    /// Actions � faire lorsque le bouton d'action est cliqu� pour l'intention de suppression
    /// </summary>
    private void DoDeleteAction() {
        this.DialogResult = DialogResult.OK;
    }

    /// <summary>
    /// Gestionnaire de l'�v�nement clic du bouton d'action.
    /// D�clenche les m�thodes d'actions sp�cifiques selon l'intention au moment
    /// de l'ouverture de la fen�tre.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void OnActionButtonClick(object? sender, EventArgs args) {
        switch (this.workingViewIntent) {
            case ViewIntentEnum.Creation:
                this.DoCreateAction();
                break;
            case ViewIntentEnum.Edition:
                this.DoEditAction();
                break;
            case ViewIntentEnum.Deletion:
                this.DoDeleteAction();
                break;
            case ViewIntentEnum.Visualization:
            default:
                this.DoVisualizeAction();
                break;
        }
    }

    /// <summary>
    /// Gestionnaire de l'�v�nement clic du bouton annuler.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelButton_Click(object sender, EventArgs e) {
        this.DialogResult = DialogResult.Cancel;
    }
}
