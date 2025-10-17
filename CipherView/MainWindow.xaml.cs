<mah:MetroWindow x:Class="CipherView.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:mah="clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:CipherView"
        mc:Ignorable="d"
        WindowStartupLocation="CenterScreen"
        Title="CipherView - Login"
        Height="406"
        Width="738"
        ResizeMode="NoResize"
        Background="{DynamicResource WindowBackground}">

    <mah:MetroWindow.Resources>
        <LinearGradientBrush x:Key="LightBackground" StartPoint="0,0" EndPoint="0,1">
            <GradientStop Color="#E9EEF7" Offset="0.0"/>
            <GradientStop Color="#D8E3F3" Offset="1.0"/>
        </LinearGradientBrush>

        <LinearGradientBrush x:Key="DarkBackground" StartPoint="0,0" EndPoint="0,1">
            <GradientStop Color="#1E1E1E" Offset="0.0"/>
            <GradientStop Color="#2B2B2B" Offset="1.0"/>
        </LinearGradientBrush>

        <StaticResource ResourceKey="LightBackground" x:Key="WindowBackground"/>

        <Style x:Key="ClassicButton" TargetType="Button">
            <Setter Property="Background" Value="#E4EBF7"/>
            <Setter Property="Foreground" Value="Black"/>
            <Setter Property="BorderBrush" Value="#A9B9D9"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="FontFamily" Value="Segoe UI"/>
            <Setter Property="FontSize" Value="13"/>
            <Setter Property="Padding" Value="4,2"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="Effect">
                <Setter.Value>
                    <DropShadowEffect Color="#A0B0D0" BlurRadius="4" ShadowDepth="1" Opacity="0.2"/>
                </Setter.Value>
            </Setter>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="Button">
                        <Border Background="{TemplateBinding Background}" 
                                BorderBrush="{TemplateBinding BorderBrush}" 
                                BorderThickness="{TemplateBinding BorderThickness}" 
                                CornerRadius="3">
                            <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
                        </Border>
                        <ControlTemplate.Triggers>
                            <Trigger Property="IsMouseOver" Value="True">
                                <Setter Property="Background" Value="#D0E2FF"/>
                                <Setter Property="BorderBrush" Value="#7FA8E5"/>
                            </Trigger>
                            <Trigger Property="IsPressed" Value="True">
                                <Setter Property="Background" Value="#BBD1F7"/>
                                <Setter Property="BorderBrush" Value="#5E87C9"/>
                            </Trigger>
                        </ControlTemplate.Triggers>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>

        <Style x:Key="ClassicTextBox" TargetType="TextBox">
            <Setter Property="BorderBrush" Value="#A9B9D9"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="Background" Value="White"/>
            <Setter Property="Foreground" Value="Black"/>
            <Setter Property="Padding" Value="3"/>
            <Style.Triggers>
                <Trigger Property="IsKeyboardFocusWithin" Value="True">
                    <Setter Property="BorderBrush" Value="#6FA8DC"/>
                    <Setter Property="Effect">
                        <Setter.Value>
                            <DropShadowEffect Color="#6FA8DC" BlurRadius="6" ShadowDepth="0" Opacity="0.5"/>
                        </Setter.Value>
                    </Setter>
                </Trigger>
            </Style.Triggers>
        </Style>

        <Style x:Key="ClassicPasswordBox" TargetType="PasswordBox">
            <Setter Property="BorderBrush" Value="#A9B9D9"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="Background" Value="White"/>
            <Setter Property="Foreground" Value="Black"/>
            <Setter Property="Padding" Value="3"/>
            <Style.Triggers>
                <Trigger Property="IsKeyboardFocusWithin" Value="True">
                    <Setter Property="BorderBrush" Value="#6FA8DC"/>
                    <Setter Property="Effect">
                        <Setter.Value>
                            <DropShadowEffect Color="#6FA8DC" BlurRadius="6" ShadowDepth="0" Opacity="0.5"/>
                        </Setter.Value>
                    </Setter>
                </Trigger>
            </Style.Triggers>
        </Style>
    </mah:MetroWindow.Resources>

    <Grid Background="{DynamicResource WindowBackground}">
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>

        <StackPanel VerticalAlignment="Top" Margin="0,40,0,0" HorizontalAlignment="Center">
            <TextBlock Text="CipherView Login" FontFamily="Segoe UI Semibold" FontSize="24" Foreground="#2C4D85" Margin="0,0,0,20"/>
            <Rectangle Fill="#9BB4E9" Height="2" Width="180"/>
        </StackPanel>

        <StackPanel VerticalAlignment="Top" HorizontalAlignment="Center" Margin="0,111,0,0">
            <Label Content="IP Address:" FontSize="13" FontWeight="Bold" Foreground="#2C4D85" Margin="0,0,0,4"/>
            <TextBox x:Name="SQLAddress" Width="220" Height="28" Margin="0,0,0,10" Style="{StaticResource ClassicTextBox}"/>
            <Label Content="Password:" FontSize="13" FontWeight="Bold" Foreground="#2C4D85" Margin="0,0,0,4"/>
            <PasswordBox x:Name="SQLPassword" Width="220" Height="28" Margin="0,0,0,10" Style="{StaticResource ClassicPasswordBox}"/>
            <Button Content="Connect" Width="100" Height="27" Margin="0,10,0,0" Style="{StaticResource ClassicButton}" Click="Button_Click"/>
        </StackPanel>

        <DockPanel Grid.Row="1" Margin="10">
            <ToggleButton x:Name="DarkModeToggle" Content="🌙 Dark Mode" Width="100" Height="25" DockPanel.Dock="Left" Style="{StaticResource ClassicButton}" Checked="DarkModeToggle_Checked" Unchecked="DarkModeToggle_Unchecked"/>
            <Button Content="FAQ" DockPanel.Dock="Left" Width="50" Height="25" Margin="5,0,0,0" Style="{StaticResource ClassicButton}" Click="Button_FAQ"/>
            <TextBlock Text="© 2025 Blazar Systems" HorizontalAlignment="Right" VerticalAlignment="Center" Foreground="#5577A9" FontSize="11"/>
        </DockPanel>
    </Grid>
</mah:MetroWindow>
