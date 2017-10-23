using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PinballClient.ClientDisplays.Dialogs;


namespace PinballClient.ClientDisplays.ModeTree
{
    public class ModeEventDeviceViewModel : Screen
    {

        #region Fields

        private string _associatedDevice;
        private ushort _associatedDeviceId;
        private IGameState _gameState;

        #endregion

        #region Properties

        public string ModeEventDeviceName { get; private set; }

        public string TypeOfDevice { get; private set; }

        public string AssociatedDevice
        {
            get
            {
                return _associatedDevice;
            }
            set
            {
                _associatedDevice = value;
                NotifyOfPropertyChange(()=> AssociatedDevice);
            }
        }

        public ushort AssociatedDeviceId
        {
            get
            {
                return _associatedDeviceId;
            }
            set
            {
                _associatedDeviceId = value;
                NotifyOfPropertyChange(() => AssociatedDeviceId);
            }
        }

        #endregion

        #region Constructor

        public ModeEventDeviceViewModel(IGameState gameState, string typeOfDevice, string modeEventDeviceName, ushort associatedDeviceId, string associatedDevice)
        {
            _gameState = gameState;
            ModeEventDeviceName = modeEventDeviceName;
            TypeOfDevice = typeOfDevice;
            _associatedDeviceId = associatedDeviceId;
            _associatedDevice = associatedDevice;
        }

        #endregion

        public async void SelectAssociatedDevice()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);

            var devices = new List<string>();
            switch (TypeOfDevice)
            {
                case "Event":
                {
                    devices = _gameState.Switches.Select(sw => sw.Name).ToList();
                    break;
                }
                case "Switch":
                {
                    devices = _gameState.Switches.Select(sw => sw.Name).ToList();
                    break;
                }
                case "Coil":
                {
                    devices = _gameState.Coils.Select(coil => coil.Name).ToList();
                    break;
                }
                default:
                {
                    devices = _gameState.Switches.Select(sw => sw.Name).ToList();
                    break;
                }

            }

            var dialog = new DeviceSelectorDialog(devices, metroWindow);
            await metroWindow.ShowMetroDialogAsync(dialog);

            var result = await dialog.WaitForButtonPressAsync();
            if (!string.IsNullOrEmpty(result))
            {
                AssociatedDevice = result;
            }
            await metroWindow.HideMetroDialogAsync(dialog);
        }
    }
}

/*
 * OLD TREEVIEW CODE WHEN REQUIRED TO ADD TO NEW EXPANDER STYLE VIEW
 * 
 * 
 * 
 * {Binding IsSelected, Converter={StaticResource BooleanConditionConverter}, ConverterParameter={StaticResource SelectedDeviceHighlight}}
SelectedValue="{Binding SelectedSwitch}"


<Binding Path="DataContext.ScaleFactorY" RelativeSource="{RelativeSource FindAncestor, AncestorType={x:Type playfield:PlayfieldView}}"/>


  <TreeView 
        Background="Transparent"
        BorderThickness="0"
        ItemsSource="{Binding FirstGeneration}">
        <TreeView.ItemContainerStyle>
            <Style TargetType="{x:Type TreeViewItem}">
                <Setter Property="IsExpanded" Value="{Binding IsExpanded, Mode=TwoWay}" />
                <Setter Property="IsSelected" Value="{Binding IsSelected, Mode=TwoWay}" />
                <Setter Property="FontWeight" Value="Normal" />
                <Style.Triggers>            
                    <Trigger Property="IsSelected" Value="True">
                           <!-- <Setter Property="FontWeight" Value="Bold" />-->                   
                    </Trigger>                    
                </Style.Triggers>                
            </Style>          
        </TreeView.ItemContainerStyle>
  
        <TreeView.Resources>
            
            <!-- Template for parent types - Categories -->
            <HierarchicalDataTemplate                     
                DataType="{x:Type DeviceTree:DeviceTypeViewModel}" 
                ItemsSource="{Binding Children}"
                >               
                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="159" />
                        <ColumnDefinition Width="70" />
                        <ColumnDefinition Width="40" />
                    </Grid.ColumnDefinitions>
                    <TextBlock 
                        Grid.Column="0"
                        Text="{Binding DeviceTypeName}" 
                        FontSize="14"
                        />
                    <Button 
                        Grid.Column="2"
                        Width="20"
                        Height="20"
                        cal:Message.Attach="ChangeVisibility()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"                      
                        >
                        <Rectangle 
                            Width="12"
                            Height="8"
                            Style="{StaticResource VisibilityIcon}"   
                            Fill="{Binding Path=Foreground, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Button}}}"/>
                    </Button>                    
                    <Grid.ContextMenu >
                            <ContextMenu>
                                <MenuItem 
                                    FontWeight="Bold" 
                                    Header="Add New Device" 
                                    cal:Message.Attach="AddDevice()"                                   
                                    />
                        </ContextMenu>
                    </Grid.ContextMenu>
                </Grid>               
            </HierarchicalDataTemplate>
            
           <!-- Template for Switches -->                        
            <DataTemplate                 
                DataType="{x:Type commonViewModels:SwitchViewModel}">                
                <Grid 
                    Tag="{Binding Path=DataContext, RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=TreeView}}"
                    >
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="140" />
                        <ColumnDefinition Width="70" />
                        <ColumnDefinition Width="40" />
                    </Grid.ColumnDefinitions>
                    <TextBlock 
                        Grid.Column="0"
                        Text="{Binding Name}" />
                    <Button
                        Grid.Column="1" 
                        Height="20"
                        BorderThickness="1"
                        Style="{StaticResource FlatButton}"
                        Content="{Binding State}"
                        cal:Message.Attach="ActivateDeviceState()"
                        VerticalContentAlignment="Top"
                        FontSize="14"
                        Margin="10,0,10,0"
                        Background="{Binding IsDeviceActive, Converter={StaticResource BooleanConditionConverter}, ConverterParameter={StaticResource StateActiveConditionalValue}}"
                        ToolTip="Activate Switch"
                        />
                    <Button 
                        Grid.Column="2"
                        Width="20"
                        Height="20"
                        cal:Message.Attach="ChangeVisibility()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        IsEnabled="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type TreeViewItem}, AncestorLevel=2}, Path=DataContext.IsVisible}"
                        >
                        <Rectangle 
                            Width="12"
                            Height="8"
                            Style="{StaticResource VisibilityIcon}"   
                            Fill="{Binding Path=Foreground, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Button}}}"/>
                    </Button>
                    <Grid.ContextMenu >
                            <ContextMenu>
                                <MenuItem 
                                    FontWeight="Bold" 
                                    Header="Configure Switch" 
                                    cal:Message.Attach="ConfigureDevice()" 
                                    />
                            </ContextMenu>
                        </Grid.ContextMenu>
                    </Grid>
                </DataTemplate>

            <!-- Template for Coils -->
            <DataTemplate 
                DataType="{x:Type commonViewModels:CoilViewModel}">
                <Grid                         
                    Tag="{Binding Path=DataContext, RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=TreeView}}"
                    >
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="140" />
                        <ColumnDefinition Width="70" />
                        <ColumnDefinition Width="40" />
                    </Grid.ColumnDefinitions>
                    <TextBlock 
                        Grid.Column="0"
                        Text="{Binding Name}" />
                    <Button
                            Grid.Column="1"
                            Height="20"
                            BorderThickness="1"
                            Style="{StaticResource FlatButton}"
                            Content="Pulse"
                            cal:Message.Attach="ActivateDeviceState()"
                            VerticalContentAlignment="Top"
                            FontSize="14"
                            Margin="10,0,10,0"
                            Background="{Binding IsDeviceActive, Converter={StaticResource BooleanConditionConverter}, ConverterParameter={StaticResource StateActiveConditionalValue}}"
                        ToolTip="Activate Coil"
                            />
                    <Button 
                        Grid.Column="2"
                        Width="20"
                        Height="20"
                        cal:Message.Attach="ChangeVisibility()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        IsEnabled="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type TreeViewItem}, AncestorLevel=2}, Path=DataContext.IsVisible}"
                        >
                        <Rectangle 
                            Width="12"
                            Height="8"
                            Style="{StaticResource VisibilityIcon}"   
                            Fill="{Binding Path=Foreground, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Button}}}"/>
                    </Button>

                    <Grid.ContextMenu >
                        <ContextMenu>
                            <MenuItem 
                                    FontWeight="Bold" 
                                    Header="Configure Coil" 
                                    cal:Message.Attach="ConfigureDevice()"
                                    
                                    />
                        </ContextMenu>
                    </Grid.ContextMenu>
                </Grid>
            </DataTemplate>

            <!-- Template for Stepper Motors -->
            <DataTemplate 
                DataType="{x:Type commonViewModels:StepperMotorViewModel}">
                <Grid 
                        Tag="{Binding Path=DataContext, RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=TreeView}}"
                        >
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="140" />
                        <ColumnDefinition Width="35" />
                        <ColumnDefinition Width="35" />
                        <ColumnDefinition Width="40" />
                    </Grid.ColumnDefinitions>
                    <TextBlock 
                        Grid.Column="0"
                            Text="{Binding Name}" />
                    <Button 
                        Grid.Column="1"
                        Width="20"
                        Height="20"
                        Margin="12,0,0,0"
                        cal:Message.Attach="RotateClockwise()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        ToolTip="Rotate Clockwise"
                        >
                        <Rectangle 
                            Width="12"
                            Height="12"
                            >
                            <Rectangle.Fill>
                                <VisualBrush Visual="{StaticResource appbar_transform_rotate_clockwise}" />
                            </Rectangle.Fill>
                        </Rectangle>
                    </Button>
                    <Button 
                        Grid.Column="2"
                        Width="20"
                        Height="20"
                        Margin="0,0,12,0"
                        cal:Message.Attach="RotateCounterClockwise()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        ToolTip="Rotate Counter Clockwise"
                        >
                        <Rectangle 
                            Width="12"
                            Height="12"
                            >
                            <Rectangle.Fill>
                                <VisualBrush Visual="{StaticResource appbar_transform_rotate_counterclockwise}" />
                            </Rectangle.Fill>
                        </Rectangle>
                    </Button>
                    <Button 
                        Grid.Column="3"
                        Width="20"
                        Height="20"
                        cal:Message.Attach="ChangeVisibility()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        IsEnabled="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type TreeViewItem}, AncestorLevel=2}, Path=DataContext.IsVisible}"
                        >
                        <Rectangle 
                            Width="12"
                            Height="8"
                            Style="{StaticResource VisibilityIcon}"   
                            Fill="{Binding Path=Foreground, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Button}}}"/>
                    </Button>

                    <Grid.ContextMenu >
                        <ContextMenu>
                            <MenuItem 
                                    FontWeight="Bold" 
                                    Header="Configure Stepper Motor" 
                                    cal:Message.Attach="ConfigureDevice()"
                                    
                                    />
                            <MenuItem 
                                    Header="Rotate Right" 
                                    cal:Message.Attach="RotateRight()"                                 
                                    />
                            <MenuItem 
                                    Header="Rotate Left" 
                                    cal:Message.Attach="RotateLeft()"                                 
                                    />
                        </ContextMenu>
                    </Grid.ContextMenu>
                </Grid>
            </DataTemplate>

            <!-- Template for Servos -->
            <DataTemplate 
                DataType="{x:Type commonViewModels:ServoViewModel}">
                <Grid 
                        Tag="{Binding Path=DataContext, RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=TreeView}}"
                        >
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="140" />
                        <ColumnDefinition Width="35" />
                        <ColumnDefinition Width="35" />
                        <ColumnDefinition Width="40" />
                    </Grid.ColumnDefinitions>
                    <TextBlock 
                        Grid.Column="0"
                            Text="{Binding Name}" />
                    <Button 
                        Grid.Column="1"
                        Width="20"
                        Height="20"
                        Margin="12,0,0,0"
                        cal:Message.Attach="RotateClockwise()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        ToolTip="Rotate Clockwise"
                        >
                        <Rectangle 
                            Width="12"
                            Height="12"
                            >   
                            <Rectangle.Fill>
                                <VisualBrush Visual="{StaticResource appbar_transform_rotate_clockwise}" />
                            </Rectangle.Fill> 
                        </Rectangle>
                    </Button>
                    <Button 
                        Grid.Column="2"
                        Width="20"
                        Height="20"
                        Margin="0,0,12,0"
                        cal:Message.Attach="RotateCounterClockwise()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        ToolTip="Rotate Counter Clockwise"
                        >
                        <Rectangle 
                            Width="12"
                            Height="12"
                            >
                            <Rectangle.Fill>
                                <VisualBrush Visual="{StaticResource appbar_transform_rotate_counterclockwise}" />
                            </Rectangle.Fill>
                        </Rectangle>
                    </Button>
                    <Button 
                        Grid.Column="3"
                        Width="20"
                        Height="20"
                        cal:Message.Attach="ChangeVisibility()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        IsEnabled="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type TreeViewItem}, AncestorLevel=2}, Path=DataContext.IsVisible}"
                        >
                        <Rectangle 
                            Width="12"
                            Height="8"
                            Style="{StaticResource VisibilityIcon}"   
                            Fill="{Binding Path=Foreground, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Button}}}"/>
                    </Button>

                    <Grid.ContextMenu >
                        <ContextMenu>
                            <MenuItem 
                                    FontWeight="Bold" 
                                    Header="Configure Servo" 
                                    cal:Message.Attach="ConfigureDevice()"
                                    
                                    />
                        </ContextMenu>
                    </Grid.ContextMenu>
                </Grid>
            </DataTemplate>
           
            <!-- Template for DC Motors -->
            <DataTemplate DataType="{x:Type commonViewModels:DCMotorViewModel}">
                <StackPanel Orientation="Horizontal">
                    <TextBlock Text="{Binding Name}" />
                </StackPanel>
            </DataTemplate>

            <!-- Template for Leds -->
            <DataTemplate 
                DataType="{x:Type commonViewModels:LedViewModel}">
                <Grid 
                        Tag="{Binding Path=DataContext, RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=TreeView}}"
                        >
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="140" />
                        <ColumnDefinition Width="35" />
                        <ColumnDefinition Width="35" />
                        <ColumnDefinition Width="40" />
                    </Grid.ColumnDefinitions>
                    <TextBlock 
                        Grid.Column="0"
                            Text="{Binding Name}" />
                    <Button 
                        Grid.Column="1"
                        Width="20"
                        Height="20"
                        Margin="12,0,0,0"
                        cal:Message.Attach="DeactivateLed()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        ToolTip="Deactivate Led"
                        >
                        <Rectangle 
                            Margin="0,2,0,0"
                            
                            Width="8"
                            Height="10"
                            >
                            <Rectangle.Fill>
                                <VisualBrush Visual="{StaticResource appbar_lightbulb_hue}" />
                            </Rectangle.Fill>
                        </Rectangle>
                    </Button>
                    <Button 
                        Grid.Column="2"
                        Width="20"
                        Height="20"
                        Margin="0,0,12,0"
                        cal:Message.Attach="ActivateLed()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        ToolTip="Activate Led"
                        >
                        <Rectangle 
                            Width="12"
                            Height="12"
                            >
                            <Rectangle.Fill>
                                <VisualBrush Visual="{StaticResource appbar_lightbulb_hue_on}" />
                            </Rectangle.Fill>
                        </Rectangle>
                    </Button>
                    <Button 
                        Grid.Column="3"
                        Width="20"
                        Height="20"
                        cal:Message.Attach="ChangeVisibility()"
                        BorderThickness="0"
                        Style="{StaticResource {x:Static ToolBar.ButtonStyleKey}}"
                        IsEnabled="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type TreeViewItem}, AncestorLevel=2}, Path=DataContext.IsVisible}"
                        >
                        <Rectangle 
                            Width="12"
                            Height="8"
                            Style="{StaticResource VisibilityIcon}"   
                            Fill="{Binding Path=Foreground, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type Button}}}"/>
                    </Button>

                    <Grid.ContextMenu >
                        <ContextMenu>
                            <MenuItem 
                                    FontWeight="Bold" 
                                    Header="Configure Led" 
                                    cal:Message.Attach="ConfigureDevice()"
                                    
                                    />
                        </ContextMenu>
                    </Grid.ContextMenu>
                </Grid>
            </DataTemplate>

        </TreeView.Resources>

        </TreeView>*/

